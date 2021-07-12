using ShoppingOnline.Common.Repository;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
    public interface ICartItemRepository : IEntityFrameworkRepository<CartItem>
    {
        IQueryable<CartItemDTO> GetCartItems(int userId);
        Task<int> DeleteCartItem(CartItemDTO cartItemDto);
    }       
}
