using Microsoft.EntityFrameworkCore;

namespace Projecto_Final.Models.ProductDTOs
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }
        public Status_product Status { get; set; } = Status_product.OK;

        [Precision(12, 2)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }

        public int? DiscountId { get; set; }
       
    }
}
