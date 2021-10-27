using TestWebApp.BLL.Repositories.Interfaces;
using TestWebApp.DAL.Models.Auth.Requests;
using TestWebApp.DAL.Models.Entities;
using TestWebApp.WebAPI.Contracts.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestWebApp.WebAPI.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        [HttpGet(ApiRoutes.Order.GetAll)]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders != null)
                return Ok(orders);

            return BadRequest();
        }

        [HttpGet(ApiRoutes.Order.Get)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                return BadRequest();

            return Ok(order);
        }


        [HttpPost(ApiRoutes.Order.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] OrderRequest request)
        {
            if (request == null)
                return BadRequest();

            var order = new Order()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Country = request.Country,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                TotalPrice = request.TotalPrice
            };

            var created = await _orderRepository.CreateAsync(order);

            if (created)
                return Ok();

            return BadRequest();
        }

        [HttpPut(ApiRoutes.Order.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] string orderId, [FromBody] OrderRequest request)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                return BadRequest();

            order.FirstName = request.FirstName;
            order.LastName = request.LastName;
            order.Country = request.Country;
            order.Email = request.Email;
            order.PhoneNumber = request.PhoneNumber;
            order.TotalPrice = request.TotalPrice;

            var updated = await _orderRepository.UpdateAsync(order);

            if (updated)
                return Ok();

            return BadRequest();
        }

        [HttpDelete(ApiRoutes.Order.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] string orderId)
        {
            if (orderId == default)
                return BadRequest();

            var deleted = await _orderRepository.DeleteAsync(orderId);

            if (deleted)
                return Ok();

            return BadRequest();
        }
    }
}
