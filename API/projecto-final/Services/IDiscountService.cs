using Projecto_Final.Models;
using Projecto_Final.Models.DiscountDTOs;

namespace Projecto_Final.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAll();
        Task<bool> Create(DiscountCreateDTO discount);
        Task<Discount> GetbyId(int id);
        Task<bool> Edit(DiscountEditDTO discount);
        Task<bool> DeleteById(int id);
    }
}
