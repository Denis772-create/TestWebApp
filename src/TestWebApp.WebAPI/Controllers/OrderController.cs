using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.BLL.Repositories.Entities.Interfaces;
using TestWebApp.DAL.Models.Auth.Request;

namespace TestWebApp.WebAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders != null)
                return Ok(orders);

            return BadRequest();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string productId)
        {
            var order = await _orderRepository.GetByIdAsync(productId);

            if (order == null)
                return BadRequest();

            return Ok(order);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] OrderRequest request)
        {
            if (request == null)
                return BadRequest();

            //var product = new Product()
            //{
            //    Title = request.Title,
            //    Description = request.Description,
            //    Price = request.Price,
            //    QuantityInStock = request.QuantityInStock,
            //    IsAvailable = request.IsAvailable,
            //};

            //var created = await _productRepository.CreateAsync(product);

            //if (created)
            //    return Ok();

            return BadRequest();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string productId, [FromBody] ProductRequest request)
        {
            //var product = await _productRepository.GetByIdAsync(productId);

            //if (product == null)
            //    return BadRequest();

            //product.Title = request.Title;
            //product.Description = request.Description;
            //product.Price = request.Price;
            //product.QuantityInStock = request.QuantityInStock;
            //product.IsAvailable = request.IsAvailable;

            //var updated = await _productRepository.UpdateAsync(product);

            //if (updated)
            //    return Ok();

            return BadRequest();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string productId)
        {
            //if (productId == default)
            //    return BadRequest();

            //var deleted = await _productRepository.DeleteAsync(productId);

            //if (deleted)
            //    return Ok();

            return BadRequest();
        }
    }
}
