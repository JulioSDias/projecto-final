using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.OrderDTOs;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO newOrder) {
            if (await _orderService.CreateOrder(newOrder) == false)
                return BadRequest();
            return Ok();
        }

        [HttpPost("item")]
        public async Task<IActionResult> CreateItem([FromForm] ItemCreateDTO newItem)
        {
            if (await _orderService.CreateItem(newItem) == false)
                return BadRequest();
            return Ok();
        }

        [HttpGet("item")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _orderService.GetItemById(id);
            if (item == null)
                return BadRequest();
            return Ok(item);
        }

        [HttpGet("allItems")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _orderService.GetAllItems();
            if (items == null)
                return BadRequest();
            return Ok(items);
        }


        [HttpGet("allOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            if (orders == null)
                return BadRequest();
            return Ok(orders);
        }
    }
}
