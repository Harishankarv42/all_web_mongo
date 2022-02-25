using MongoDB.Bson;

namespace all_web_mongo.Models
{
    public class Products
    {
        public ObjectId Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescirption { get; set; }
        public string Image { get; set; }
    }
}
