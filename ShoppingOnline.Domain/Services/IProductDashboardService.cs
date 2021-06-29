using ShoppingOnline.Common.Models;
using ShoppingOnline.DTO;
using System.Linq;

namespace ShoppingOnline.Domain.Services
{
   public interface IProductDashboardService
    {
        ResponseModel<IQueryable<ProductDTO>> GetAllProducts();
    }
}
