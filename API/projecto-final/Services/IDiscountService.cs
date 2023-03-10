using Projecto_Final.Models;
using Projecto_Final.Models.DiscountDTOs;

namespace Projecto_Final.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<ProductDiscount>> GetAll();
        Task<bool> Create(DiscountCreateDTO discount);
        Task<DiscountReturnDTO> GetbyId(int id);
        Task<bool> Edit(DiscountCreateDTO discount);
    }
}
