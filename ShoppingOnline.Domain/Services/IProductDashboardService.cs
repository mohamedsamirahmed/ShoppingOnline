using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services
{
   public interface IProductDashboardService
    {
        Task<ResponseModel<PagedList<ProductDTO>>> GetAllProducts(ProductParams productParams);
        ResponseModel<IQueryable<LookupDTO>> GetCategoryLookup();
    }
}
