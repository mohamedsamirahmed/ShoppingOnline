using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;

namespace ShoppingOnline.Domain.Repositories
{
   public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        public ProductRepository(ShoppingOnlineDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
