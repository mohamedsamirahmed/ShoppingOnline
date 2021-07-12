using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    class CartRepository : EntityFrameworkRepository<Cart>, ICartRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        public CartRepository(ShoppingOnlineDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Cart> ActiveCartExist(int userId)
        {
            var cartStatusRecord = await this.GetAll().Include(cs => cs.CartStatus)
               .SingleOrDefaultAsync(x => x.UserId == userId && x.CartStatus.Name == "New");

            return cartStatusRecord;
        }

        public async Task<int> AddCart(int cartStatusId, int userId)
        {
            var cart = new Cart()
            {
                UserId = userId,
                CartStatusId = cartStatusId
            };
            _dbContext.Carts.Add(cart);

           return  await _dbContext.SaveChangesAsync();
        }
    }
}
