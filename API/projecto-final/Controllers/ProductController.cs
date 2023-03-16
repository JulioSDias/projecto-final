using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.DiscountDTOs;
using Projecto_Final.Models.ProductDTOs;
using Projecto_Final.Models.UserDTOs;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO newProduct)
        {
            if (await _productService.Create(newProduct) == true)
                return Ok("Product created.");
            return BadRequest("failed to create product.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetbyId(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("deletebyID/{id}")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            if (await _productService.Delete(id) == true)
                return Ok("product deleted.");
            return NotFound("product not found.");
        }

        [HttpGet("getallconnections")]
        public async Task<IActionResult> GetAllConnections() {
            return Ok(await _productService.GetAllConnections());
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductUpdateDTO newChanges)
        {
            if (await _productService.Update(newChanges) == false)
                return NotFound("not found");
            return Ok("updated");
        }

    }
}
