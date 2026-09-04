using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeToMongo.Models
{
    public class ConfigStrings
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string MongoConnectionString { get; set; } = string.Empty;
        public string DbName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
        
    }
}
