using Projecto_Final.Models.OrderDTOs;

namespace Projecto_Final.Services
{
    public interface IOrderService
    {
        Task<bool> CreateItem(ItemCreateDTO newItem);
        Task<ItemReturnDTO> GetItemById(int id);
    }
}
