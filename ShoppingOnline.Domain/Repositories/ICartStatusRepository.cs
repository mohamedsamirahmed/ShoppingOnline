using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
    public interface ICartStatusRepository : IEntityFrameworkRepository<CartStatus>
    {
        Task<CartStatus> GetCartStatusByName(string key);
    }
}
