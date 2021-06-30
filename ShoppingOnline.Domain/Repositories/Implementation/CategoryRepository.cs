using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.Domain.Repositories.Implementation
{
    public class CategoryRepository : EntityFrameworkRepository<Category>, ICategoryRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        public CategoryRepository(ShoppingOnlineDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
