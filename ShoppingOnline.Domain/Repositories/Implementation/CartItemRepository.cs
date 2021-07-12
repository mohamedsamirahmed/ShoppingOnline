using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories.Implementation
{
    public class CartItemRepository : EntityFrameworkRepository<CartItem>, ICartItemRepository
    {
        public ShoppingOnlineDBContext _dbContext { get; }
        public IMapper _mapper { get; }

        public CartItemRepository(ShoppingOnlineDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public IQueryable<CartItemDTO> GetCartItems(int userId)
        {
            return this.GetAll().Include(c => c.Cart)
                .Include(p => p.Product)
                .Where(c => c.Cart.CartStatus.Name == "New" && c.Cart.UserId == userId)
                .ProjectTo<CartItemDTO>(_mapper.ConfigurationProvider);
        }

        public async Task<int> DeleteCartItem(CartItemDTO cartItemDto)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            _dbContext.CartItems.Remove(cartItem);
            return await _dbContext.SaveChangesAsync();
        }

    }
}
