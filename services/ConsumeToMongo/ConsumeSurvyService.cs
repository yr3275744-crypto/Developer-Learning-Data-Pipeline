using Confluent.Kafka;
using Confluent.Kafka.Admin;
using ConsumeToMongo.Models;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace ConsumeToMongo
{
    public class ConsumeSurvyService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IMongoCollection<Survy> _survyCollection;
        private readonly ConfigStrings _configStrings;
        public ConsumeSurvyService(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            _configStrings = scope.ServiceProvider.GetRequiredService<ConfigStrings>();
            
            _survyCollection = new MongoClient(_configStrings.MongoConnectionString)
                .GetDatabase(_configStrings.DbName)
                .GetCollection<Survy>(_configStrings.CollectionName);
            
            var config = new ConsumerConfig
            {
                BootstrapServers = _configStrings.BootstrapServers,
                GroupId = _configStrings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }
        
        public async Task ConsumeToDbLoop()
        {
            try
            {
                _consumer.Subscribe(_configStrings.Topic);
                while (true)
                {
                    var cr = _consumer.Consume(TimeSpan.FromSeconds(3));
                    if (cr == null || cr.Message.Value == null)
                    {
                        continue;
                    }
                    Console.WriteLine($"consume: {cr.Message.Value}");
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Survy? survy = JsonSerializer.Deserialize<Survy>(cr.Message.Value, options);
                    if (survy is null)
                    {
                        continue;
                    }
                    await _survyCollection.InsertOneAsync(survy);
                }
            }
            finally
            {
                _consumer.Unsubscribe();
                _consumer.Dispose();
            }
        }
        //public void Subscribe(string topic)
        //{
        //    _consumer.Subscribe(topic);
        //}
        //public void Dispose()
        //{
        //    _consumer.Unsubscribe();
        //    _consumer.Dispose();
        //}
    }
}
