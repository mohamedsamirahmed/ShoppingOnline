using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class CategoryRepository : EntityFrameworkRepository<Category>, ICategoryRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(ShoppingOnlineDBContext dbContext,IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<LookupDTO> GetAllCategories()
        {
            return this.GetAll().ProjectTo<LookupDTO>(_mapper.ConfigurationProvider);
        }
    }
}
