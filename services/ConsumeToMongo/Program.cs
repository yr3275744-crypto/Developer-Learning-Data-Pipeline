using ConsumeToMongo;
using ConsumeToMongo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    public async static Task Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

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