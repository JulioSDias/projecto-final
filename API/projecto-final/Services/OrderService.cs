using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;
using Projecto_Final.Models.OrderDTOs;
using System.Data;
//Update order on create item
//Doesnt save items in create order method
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

            var DBorder = new Order
            {
                ClientName = newOrder.ClientName,
                AddressLine = newOrder.AddressLine,
                Status = newOrder.Status,
                CreatedDate = DateTimeOffset.Now,
                UserId = newOrder.UserId,
            };

            await _context.Orders.AddAsync(DBorder);
            await _context.SaveChangesAsync();

            decimal total = 0;
            var items = new List<OrderItem>();

            foreach (var item in newOrder.Items) {
                var product = await _productService.GetbyId(item.ProductId);

                if (item.Quantity > product.Stock)
                    return false;

                var DBItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    Price = item.Price,
                    CreatedDate = DateTimeOffset.Now,
                    ProductId = item.ProductId,
                    OrderId = DBorder.Id
                };
                total += ((item.Price * (1 - product.Discount.DiscountPercent)) * item.Quantity);
                items.Add(DBItem);

                await _context.Order_Items.AddAsync(DBItem);
            }
            DBorder.Total = total;
            DBorder.Items = items;
            await _context.SaveChangesAsync();


            

            return true;
        }

        public async Task<IEnumerable<ItemReturnDTO>> GetAllItems() {
            var items = await _context.Order_Items.ToListAsync();
            var returnItems = new List<ItemReturnDTO>();
            foreach (var item in items) {
                var returnItem = new ItemReturnDTO
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                    ProductId = item.ProductId,
                    OrderId = item.OrderId,
                };
                returnItems.Add(returnItem);
            }

                return returnItems;
        }

        public async Task<IEnumerable<OrderReturnDTO>> GetAllOrders() {
            var orders = await _context.Orders.Include(i => i.Items).ToListAsync();
            var returnOrders = new List<OrderReturnDTO>();
            foreach (var order in orders) {
                var returnItems = new List<ItemReturnDTO>();
                foreach (var item in order.Items) {
                    var returnItem = new ItemReturnDTO
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,
                        ProductId = item.ProductId,
                        OrderId = item.OrderId,
                    };
                    returnItems.Add(returnItem);
                }
                var returnOrder = new OrderReturnDTO
                {
                    Id = order.Id,
                    ClientName = order.ClientName,
                    AddressLine = order.AddressLine,
                    Status = order.Status,
                    Total = order.Total,
                    CreatedDate = order.CreatedDate,
                    ModifiedDate = order.ModifiedDate,
                    UserId = order.UserId,
                    Items = returnItems
                };
                returnOrders.Add(returnOrder);
            }
            return returnOrders;
        }

        public async Task<OrderReturnDTO> GetOrderById(Guid id) {
            var order = await _context.Orders.Include(i => i.Items).FirstOrDefaultAsync(x => x.Id == id);
           
                var returnItems = new List<ItemReturnDTO>();
                foreach (var item in order.Items)
                {
                    var returnItem = new ItemReturnDTO
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,
                        ProductId = item.ProductId,
                        OrderId = item.OrderId,
                    };
                    returnItems.Add(returnItem);
                }
                var returnOrder = new OrderReturnDTO
                {
                    Id = order.Id,
                    ClientName = order.ClientName,
                    AddressLine = order.AddressLine,
                    Status = order.Status,
                    Total = order.Total,
                    CreatedDate = order.CreatedDate,
                    ModifiedDate = order.ModifiedDate,
                    UserId = order.UserId,
                    Items = returnItems
                };
                
         
            return returnOrder;
        }
    }
}
