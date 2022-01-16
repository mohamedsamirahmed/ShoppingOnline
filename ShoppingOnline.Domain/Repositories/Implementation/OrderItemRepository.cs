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

namespace ShoppingOnline.Domain.Repositories.Implementation
{
    public class OrderItemRepository : EntityFrameworkRepository<OrderItems>, IOrderItemRepository
    {

        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        public OrderItemRepository(ShoppingOnlineDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<OrderItemDTO> GetOrderItems(int userId)
        {
            return this.GetAll().Include(o => o.Order)
                .Include(p => p.Product)
                .Where(o => o.Order.UserId == userId)
                .ProjectTo<OrderItemDTO>(_mapper.ConfigurationProvider);
        }

    }
}
