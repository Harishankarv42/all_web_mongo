using all_web_mongo.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace all_web_mongo.Models
{
    public class Context : IContext
    {
        private readonly IMongoDatabase mongoDatabase;

        public Context(IOptions<ConnectionSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase=client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Products> Products => mongoDatabase.GetCollection<Products>("Product");
    }
}
