using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.DiscountDTOs;
using Projecto_Final.Models.UserDTOs;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService) {
            _discountService = discountService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() {
           var discounts = await _discountService.GetAll();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var discount = await _discountService.GetbyId(id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DiscountCreateDTO newDiscount) {
            if (await _discountService.Create(newDiscount) == true)
                return Ok("discount created.");
            return BadRequest("failed to create discount.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(DiscountEditDTO newChanges)
        {
            if (await _discountService.Edit(newChanges) == false)
                return NotFound();
            return Ok();
        }

        [HttpDelete("deletebyID/{id}")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            if (await _discountService.DeleteById(id) == true)
                return Ok("Discount deleted.");
            return NotFound("Discount not found.");
        }

    }
}
