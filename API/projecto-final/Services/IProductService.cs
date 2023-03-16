using Projecto_Final.Models;
using Projecto_Final.Models.ProductDTOs;

namespace Projecto_Final.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReturnDTO>> GetAll();
        Task<bool> Create(ProductCreateDTO newProduct);
        Task<ProductReturnDTO> GetbyId(int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<ProductGenre>> GetAllConnections();
        Task<bool> Update(ProductUpdateDTO prodUpdate);
    }
}
