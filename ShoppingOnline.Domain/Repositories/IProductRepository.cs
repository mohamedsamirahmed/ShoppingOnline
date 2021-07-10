using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
    public interface IProductRepository : IEntityFrameworkRepository<Product>
    {
        IQueryable<ProductDTO> GetAllProductInfo();
        Task<ProductDTO> GetProductInfo(int productId);
    }
}
