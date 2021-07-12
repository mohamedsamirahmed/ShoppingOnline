using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories.Implementation
{
    public class OrderRepository : EntityFrameworkRepository<Order>, IOrderRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        public OrderRepository(ShoppingOnlineDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
       
        public async Task<OrderDTO> GetAllOrders(int orderId)
        {
            return await this.GetAll()
              .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(x => x.Id == orderId);
        }

        public IQueryable<OrderDTO> GetAllOrders()
        {
            return this.GetAll().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider);
        }

        public async Task<Order> ActiveOrderExist(int userId)
        {
            var cartStatusRecord = await this.GetAll().Include(cs => cs.OrderStatus)
               .SingleOrDefaultAsync(x => x.UserId == userId && x.OrderStatus.Name == "New");

            return cartStatusRecord;
        }

        public async Task<int> AddOrder(int orderStatusId, int userId)
        {
            var order = new Order()
            {
                UserId = userId,
                OrderStatusId = orderStatusId
            };
            _dbContext.Orders.Add(order);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
