using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories.Implementation
{
    public class CartStatusRepository: EntityFrameworkRepository<CartStatus>, ICartStatusRepository
    {
        public CartStatusRepository(ShoppingOnlineDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<CartStatus> GetCartStatusByName(string key)
        {
            return await this.GetAll().SingleOrDefaultAsync(x => x.Name == key);
        }
    }
}
