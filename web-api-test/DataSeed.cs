using all_web_mongo.Interface;
using all_web_mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_api_test
{
    public class DataSeed : IProductRepository
    {
        private readonly List<Products> _products;
        public DataSeed()
        {
            _products = new List<Products>()
            {
                new Products() { Id = new MongoDB.Bson.ObjectId(),ProductName="test1",ProductDescirption="desctest" }
            };
        }

        public Task<IEnumerable<Products>> GetAllProducts()
        {
            return Task.FromResult<IEnumerable<Products>>(_products);
        }

        public Task<Products> GetProductsById(int id)
        {
            var data=  _products.Where(a => a.ProductId == id)
              .FirstOrDefault();

            return Task.FromResult<Products>(data);
        }

        public Task SaveProduct(Products product)
        {
            product.Id = new MongoDB.Bson.ObjectId();
            _products.Add(product);
            return Task.FromResult(_products);
        }

        public void UpdateProduct(Products product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            var existing = _products.First(a => a.ProductId == id);
            _products.Remove(existing);
        }
    }
}
