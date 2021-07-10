using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Repositories
{
   public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
    {
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ShoppingOnlineDBContext dbContext,IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<ProductDTO> GetAllProductInfo()
        {
            return this.GetAll().Include(p => p.Photos).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider);
        }

        public async Task<ProductDTO> GetProductInfo(int productId)
        {
            return await  this.GetAll().Include(p=>p.Photos)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x=>x.Id==productId);
        }
    }
}
