using Projecto_Final.Contexts;
using Projecto_Final.Models;
using Projecto_Final.Models.OrderDTOs;


namespace Projecto_Final.Services
{
    public class OrderService : IOrderService
    {
        private readonly StoreContext _context;
        private readonly IProductService _productService;

        public OrderService(StoreContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<bool> CreateItem(ItemCreateDTO newItem) {
            var DBItem = new OrderItem
            {
                Quantity = newItem.Quantity,
                Price = newItem.Price,
                CreatedDate = DateTimeOffset.Now,
                ProductId = newItem.ProductId,
                OrderId = newItem.OrderId
            };

            await _context.Order_Items.AddAsync(DBItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ItemReturnDTO> GetItemById(int id) {
            var DBItem = await _context.Order_Items.FindAsync(id);
            if (DBItem == null) return null;
            var returnItem = new ItemReturnDTO
            {
                Id = DBItem.Id,
                Quantity = DBItem.Quantity,
                Price = DBItem.Price,
                CreatedDate = DBItem.CreatedDate,
                ModifiedDate = DBItem.ModifiedDate,
                ProductId = DBItem.ProductId,
                OrderId = DBItem.OrderId,
            };

            return returnItem;
        }

        public async Task<bool> CreateOrder(OrderCreateDTO newOrder) {

            decimal total = 0;
            var items = new List<OrderItem>();
            foreach (var id in newOrder.OrderIds) {
                var item = await _context.Order_Items.FindAsync(id);
                if (item == null) return false;
                var product = await _productService.GetbyId(item.ProductId);
                if (product == null) return false;
                total += (item.Price * product.Discount.DiscountPercent);
                items.Add(item);
            }

            var DBorder = new Order
            {
                ClientName = newOrder.ClientName,
                AddressLine = newOrder.AddressLine,
                Status = newOrder.Status,
                Total = total,
                CreatedDate = DateTimeOffset.Now,
                UserId = newOrder.UserId,
                Items = items,
            };

            await _context.Orders.AddAsync(DBorder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
