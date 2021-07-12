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
    public class OrderStatusRepository : EntityFrameworkRepository<OrderStatus>, IOrderStatusRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;

        public OrderStatusRepository(ShoppingOnlineDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<LookupDTO> GetAllOrderStatus()
        {
            return this.GetAll().ProjectTo<LookupDTO>(_mapper.ConfigurationProvider);
        }

        public async Task<OrderStatus> GetOrderStatusByName(string key)
        {
            return await this.GetAll().SingleOrDefaultAsync(x => x.Name == key);
        }
    }
}
