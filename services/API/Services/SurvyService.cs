using API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services
{
    public class SurvyService
    {
        private readonly IMongoCollection<DeveloperAnswer> _collection;
        public SurvyService(IOptions<ConfigStrings> options)
        {
            _collection = new MongoClient(options.Value.ConnectionString)
                .GetDatabase(options.Value.DbName)
                .GetCollection<DeveloperAnswer>(options.Value.CollectionName);
        }
        public async Task<IEnumerable<DeveloperAnswer>> GetAll()
        {
            return await _collection.Find(x => x.ResponseId != null).ToListAsync();
        }
    }
}
