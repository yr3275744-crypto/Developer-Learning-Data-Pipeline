using Confluent.Kafka;
using Confluent.Kafka.Admin;
using ConsumeToMongo;
using ConsumeToMongo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    //public async Task CreateTopicIfNotExists()
    //{
    //    var topic = "clean-developer-lerning-data";

    //    var adminConfig = new AdminClientConfig
    //    {
    //        BootstrapServers = "kafka:9092"
    //    };

    //    using var admin = new AdminClientBuilder(adminConfig).Build();

    //    try
    //    {
    //        await admin.CreateTopicsAsync(new[]
    //        {
    //                new TopicSpecification
    //                {
    //                    Name = topic,
    //                    NumPartitions = 3,
    //                    ReplicationFactor = 1
    //                }
    //            });

    //        Console.WriteLine($"Created topic: {topic}");
    //    }
    //    catch (CreateTopicsException e)
    //    {
    //        if (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
    //        {
    //            Console.WriteLine($"Topic already exists: {topic}");
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }
    //}
    public async static Task Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var adminConfig = new AdminClientConfig
        {
            BootstrapServers = config["kafka:BootstrapServers"]!
        };
        var topic = config["kafka:Topic"]!;
        while (true)
        {
            try
            {
                using var admin = new AdminClientBuilder(adminConfig).Build();

                var metadata = admin.GetMetadata(topic, TimeSpan.FromSeconds(5));

                if (metadata.Topics.Count > 0 &&
                    metadata.Topics[0].Error.Code == ErrorCode.NoError)
                {
                    Console.WriteLine($"Topic is ready: {topic}");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Waiting for topic: {ex.Message}");
            }

            await Task.Delay(1000);
        }
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(sp => new ConfigStrings
        {
            BootstrapServers = config["kafka:BootstrapServers"]!,
            Topic = config["kafka:Topic"]!,
            GroupId = config["kafka:GroupId"]!,
            MongoConnectionString = config["Mongo:ConnectionString"]!,
            DbName = config["Mongo:DbName"]!,
            CollectionName = config["Mongo:CollectionName"]!
        });

        serviceCollection.AddScoped(sp => new ConsumeSurvyService(sp));
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        await serviceProvider.GetRequiredService<ConsumeSurvyService>().ConsumeToDbLoop();
    }
}