using all_web_mongo.Interface;
using all_web_mongo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace all_web_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<JsonResult> GetProduct()
        {
            var result = await _productRepository.GetAllProducts();
            return new JsonResult(result);
        }


        [HttpGet("{id}")]
        public async Task<JsonResult> GetProduct(int id)
        {
            var result = await _productRepository.GetProductsById(id);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveProduct(Products product)
        {
             await _productRepository.SaveProduct(product);
            return new JsonResult("Saved Successfully");
        }

        [HttpPut]
        public Task<JsonResult> UpdateProduct(Products product)
        {
             _productRepository.UpdateProduct(product);
            return Task.FromResult(new JsonResult("Updated Successfully"));
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            return new JsonResult("Deleted Successfully");
        }
    }
}
