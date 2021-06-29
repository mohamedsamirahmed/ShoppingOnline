using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.Domain.Repositories
{
    public interface IProductRepository : IEntityFrameworkRepository<Product>
    {
    }
}
