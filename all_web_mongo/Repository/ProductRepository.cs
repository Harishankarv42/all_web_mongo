using all_web_mongo.Interface;
using all_web_mongo.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace all_web_mongo.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IContext _context;

        public ProductRepository(IContext context)
        {
            _context = context; 
        }

        public void DeleteProduct(int id)
        {
            _context.Products.DeleteOneAsync(Builders<Products>.Filter.Eq("ProductId", id));

        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Products> GetProductsById(int id)
        { 
            return await _context.Products.Find(pd => pd.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task SaveProduct(Products product)
        {
            var items = _context.Products.Find(_ => true).ToListAsync();
            product.ProductId = items.Result.Count() + 1;
            await _context.Products.InsertOneAsync(product);
        }

        public void UpdateProduct(Products product)
        {
            var filter = Builders<Products>.Filter.Eq(s => s.ProductId, product.ProductId);
            var update = Builders<Products>.Update
                            .Set(s => s.ProductName, product.ProductName)
                            .Set(s => s.ProductDescirption, product.ProductDescirption);


            _context.Products.UpdateOneAsync(filter, update);
        }
    }
}
