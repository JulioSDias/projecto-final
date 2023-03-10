using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;
using Projecto_Final.Models.DiscountDTOs;

namespace Projecto_Final.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly StoreContext _context;
        public DiscountService(StoreContext context) {

            _context = context;
        }

        public async Task<IEnumerable<ProductDiscount>> GetAll()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<bool> Create(DiscountCreateDTO discount) {

            var newDiscount = new ProductDiscount
            {
                Description = discount.Description,
                DiscountPercent = discount.DiscountPercent,
                Active = discount.Active,
                CreatedDate = DateTimeOffset.Now
            };

            await _context.Discounts.AddAsync(newDiscount);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<DiscountReturnDTO> GetbyId(int id) {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return null;

            var returnDiscount = new DiscountReturnDTO
            {
                Id = discount.Id,
                Description = discount.Description,
                DiscountPercent = discount.DiscountPercent,
            };

            return returnDiscount;
        }

        public async Task<bool> Edit(DiscountCreateDTO discount) { 
            var DBdiscount = _context.Discounts.Find();
            if (discount == null) return false;
            
            return true;
        }

    }
}
