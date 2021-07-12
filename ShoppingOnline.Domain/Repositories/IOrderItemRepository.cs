using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingOnline.Domain.Repositories
{
    public interface IOrderItemRepository : IEntityFrameworkRepository<OrderItems>
    {
        IQueryable<OrderItemDTO> GetOrderItems(int userId);
    }
}
