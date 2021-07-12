using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
    public interface ICartRepository : IEntityFrameworkRepository<Cart>
    {
        Task<Cart> ActiveCartExist(int userId);
        Task<int> AddCart(int carStatustId, int userId);
    }
}
