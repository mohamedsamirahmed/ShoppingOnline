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
        #endregion

        #region Repositories Property
        public ICategoryRepository CategoryRepo
        {
            get
            {
                if (_produCategoryRepo == null)
                {
                    _produCategoryRepo = new CategoryRepository(_dbContext);
                }
                return _produCategoryRepo;
            }
        }

        private ICategoryRepository _produCategoryRepo;

        #endregion

        #region Constructor
        public ProductCategoryService(ShoppingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GetAllProductCategories()
        {
            
        }

       
        #endregion



    }
}
