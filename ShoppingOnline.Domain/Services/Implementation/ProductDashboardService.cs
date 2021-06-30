using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.Domain.Repositories.Implementation;
using ShoppingOnline.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services.Implementation
{
    public class ProductDashboardService : IProductDashboardService
    {
        #region Property Declaration
        private readonly ShoppingOnlineDBContext _dbContext;
        #endregion

        #region Repositories Property
        public IProductRepository productRepo
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = new ProductRepository(_dbContext);
                }
                return _productRepo;
            }
        }
        
        private IProductRepository _productRepo;

        public ICategoryRepository categoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                {
                    _categoryRepo = new CategoryRepository(_dbContext);
                }
                return _categoryRepo;
            }
        }

        private ICategoryRepository _categoryRepo;
        #endregion

        #region Constructor
        public ProductDashboardService(ShoppingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region public Operations

        public async Task<ResponseModel<PagedList<ProductDTO>>> GetAllProducts(ProductParams productParams)
        {
            ResponseModel<PagedList<ProductDTO>> returnResponse = new ResponseModel<PagedList<ProductDTO>>();

            try
            {
                IQueryable<ProductDTO> products = productRepo.GetAll().Select(p => new ProductDTO() { Id = p.Id,Name = p.Name,Description=p.Description,Price=p.Price, 
                    CategoryId = p.ProductCategories.Where(pc=>pc.ProductId==p.Id).FirstOrDefault().CategoryId});

                int categoryId = 0;
                if (!string.IsNullOrEmpty(productParams.Category) && int.TryParse(productParams.Category, out categoryId))
                {
                    products = products.Where(p => p.CategoryId == categoryId);
                }
                
                var pagedproducts = await PagedList<ProductDTO>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);

                returnResponse.Entity = pagedproducts;
                returnResponse.ReturnStatus = true;
                return returnResponse;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return returnResponse;
            }
        }


        /// <summary>
        /// get all customers as lookup
        /// </summary>
        /// <returns>lookup collecion with text and value</returns>
        public ResponseModel<IQueryable<LookupDTO>> GetCategoryLookup()
        {
            ResponseModel<IQueryable<LookupDTO>> returnResponse = new ResponseModel<IQueryable<LookupDTO>>();
            try
            {
                IQueryable<LookupDTO> categories = categoryRepo.GetAll().Select(c => new LookupDTO() { text = c.Name, value = c.Id.ToString() });
                returnResponse.Entity = categories;
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            return returnResponse;
        }
        #endregion
    }
}
