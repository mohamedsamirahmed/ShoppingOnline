using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
    public interface IOrderRepository : IEntityFrameworkRepository<Order>
    {
        IQueryable<OrderDTO> GetAllOrders();
        Task<OrderDTO> GetAllOrders(int orderId);
        Task<Order> ActiveOrderExist(int userId);
        Task<int> AddOrder(int orderStatusId, int userId);
    }
}
