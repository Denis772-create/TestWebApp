using TestWebApp.BLL.Repositories.Interfaces;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Entities;
using TestWebApp.WebAPI.Contracts.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace TestWebApp.WebAPI.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet(ApiRoutes.Product.GetAll)]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            if (products != null)
                return Ok(products);

            return BadRequest("No data in the database");
        }

        [HttpGet(ApiRoutes.Product.Get)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return BadRequest("Product not found");

            return Ok(product);
        }


        [HttpPost(ApiRoutes.Product.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(ee => ee.ErrorMessage)));

            var productDto = new Product()
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
                IsAvailable = request.IsAvailable,
                ProductCategory = new ProductCategory { ProductName = request.ProductCategoryName }
            };

            var created = await _productRepository.CreateAsync(productDto);

            if (created)
                return Ok();

            return BadRequest("Such a product already exists");
        }

        [HttpPut(ApiRoutes.Product.Update)]
        public async Task<IActionResult> UpdateAsync([FromQuery] string productId, [FromBody] ProductRequest request)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return BadRequest();

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.QuantityInStock = request.QuantityInStock;
            product.IsAvailable = request.IsAvailable;

            var updated = await _productRepository.UpdateAsync(product);

            if (updated)
                return Ok();

            return BadRequest("Failed to update product");
        }

        [HttpDelete(ApiRoutes.Product.Delete)]
        public async Task<IActionResult> DeleteAsync([FromQuery] string productId)
        {
            if (productId == default)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(ee => ee.ErrorMessage)));

            var deleted = await _productRepository.DeleteAsync(productId);

            if (deleted)
                return Ok();

            return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(ee => ee.ErrorMessage))); ;
        }
    }
}
