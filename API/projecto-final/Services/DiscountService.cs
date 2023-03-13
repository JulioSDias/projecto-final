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

        public async Task<IEnumerable<Discount>> GetAll()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<bool> Create(DiscountCreateDTO discount) {

            var newDiscount = new Discount
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

        public async Task<Discount> GetbyId(int id) {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return null;

            return discount;
        }

        public async Task<bool> Edit(DiscountEditDTO discount) { 
            var DBdiscount = await _context.Discounts.FindAsync(discount.Id);
            if (DBdiscount == null) return false;
            
            DBdiscount.Description = discount.Description;
            DBdiscount.DiscountPercent = discount.DiscountPercent;
            DBdiscount.Active = discount.Active;
            DBdiscount.ModifiedDate = DateTimeOffset.Now;

            _context.Discounts.Entry(DBdiscount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var DBdiscount = await _context.Discounts.FindAsync(id);
            if (DBdiscount == null) return false;

            _context.Discounts.Remove(DBdiscount);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
