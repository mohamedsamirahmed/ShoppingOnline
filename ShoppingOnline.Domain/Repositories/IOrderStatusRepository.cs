using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
    public interface IOrderStatusRepository : IEntityFrameworkRepository<OrderStatus>
    {
        IQueryable<LookupDTO> GetAllOrderStatus();
        Task<OrderStatus> GetOrderStatusByName(string key);
    }
}
