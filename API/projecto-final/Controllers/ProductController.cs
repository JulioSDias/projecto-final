using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.DiscountDTOs;
using Projecto_Final.Models.ImageDTOs;
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
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
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
                return Ok();
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

        [HttpPost("uploadImage")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO newImage)
        {
            await _imageService.UploadImage(newImage);
            return Ok("uploaded");
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetByIdImage(int id)
        {
            var image = await _imageService.GetById(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }

        [HttpGet("image/{productId}")]
        public async Task<IActionResult> GetAllImages(int productId)
        {
            var images = await _imageService.GetImagesByProductId(productId);
            if (images == null)
                return BadRequest();
            return Ok(images);
        }

        [HttpDelete("image/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            if (await _imageService.Delete(id) == false)
                return NotFound("image not found");
            return Ok("image deleted");
        }
    }
}
