using TestWebApp.BLL.Repositories.Entities.Interfaces;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestWebApp.WebAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            if (products != null)
                return Ok(products);

            return BadRequest();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return BadRequest();

            return Ok(product);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductRequest request)
        {
            if (request == null)
                return BadRequest();

            var product = new Product()
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
                IsAvailable = request.IsAvailable
            };

            var created = await _productRepository.CreateAsync(product);

            if (created)
                return Ok();

            return BadRequest();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string productId, [FromBody] ProductRequest request)
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

            return BadRequest();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string productId)
        {
            if (productId == default)
                return BadRequest();

            var deleted = await _productRepository.DeleteAsync(productId);

            if (deleted)
                return Ok();

            return BadRequest();
        }
    }
}
