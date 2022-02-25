using all_web_mongo.Models;
using MongoDB.Driver;

namespace all_web_mongo.Interface
{
    public interface IContext
    {
        IMongoCollection<Products> Products { get; }
    }
}
