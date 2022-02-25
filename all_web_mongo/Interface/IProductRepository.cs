using all_web_mongo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace all_web_mongo.Interface
{
    public interface IProductRepository
    {
       Task<IEnumerable<Products>> GetAllProducts();
       Task<Products> GetProductsById(int id);
       Task SaveProduct(Products product);
       void UpdateProduct(Products product);
        void  DeleteProduct(int id);

    }
}
