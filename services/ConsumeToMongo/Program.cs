using Microsoft.Extensions.Configuration;

class Program
{
    public async static Task Main()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
}