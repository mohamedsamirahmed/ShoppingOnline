using AutoMapper;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.Domain.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.Domain.Services.Implementation
{
    class ProductCategoryService:IProductCategoryService
    {
        #region Property Declaration
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        #endregion

        #region Repositories Property
        public ICategoryRepository CategoryRepo
        {
            get
            {
                if (_produCategoryRepo == null)
                {
                    _produCategoryRepo = new CategoryRepository(_dbContext,_mapper);
                }
                return _produCategoryRepo;
            }
        }

        private ICategoryRepository _produCategoryRepo;

        #endregion

        #region Constructor
        public ProductCategoryService(ShoppingOnlineDBContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void GetAllProductCategories()
        {
            
        }

       
        #endregion



    }
}
