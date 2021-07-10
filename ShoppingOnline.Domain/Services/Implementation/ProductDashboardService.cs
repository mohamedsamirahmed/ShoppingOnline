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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services.Implementation
{
    public class ProductDashboardService : IProductDashboardService
    {
        #region Property Declaration
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        #endregion

        #region Repositories Property
        public IProductRepository productRepo
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = new ProductRepository(_dbContext,_mapper);
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
                    _categoryRepo = new CategoryRepository(_dbContext,_mapper);
                }
                return _categoryRepo;
            }
        }

        private ICategoryRepository _categoryRepo;
        #endregion

        #region Constructor
        public ProductDashboardService(ShoppingOnlineDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region public Operations

        public async Task<ResponseModel<PagedList<ProductDTO>>> GetAllProducts(ProductParams productParams)
        {
            ResponseModel<PagedList<ProductDTO>> returnResponse = new ResponseModel<PagedList<ProductDTO>>();

            try
            {
                IQueryable<ProductDTO> productsDTO = productRepo.GetAllProductInfo();
                //var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

                int categoryId = 0;
                if (!string.IsNullOrEmpty(productParams.Category) && int.TryParse(productParams.Category, out categoryId))
                {
                    productsDTO = productsDTO.Where(p => p.CategoryId == categoryId);
                }

                var pagedproducts = PagedList<ProductDTO>.CreateAsync(productsDTO, productParams.PageNumber, productParams.PageSize);

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


        public ResponseModel<List<LookupDTO>> GetCategoryLookup()
        {
            ResponseModel<List<LookupDTO>> returnResponse = new ResponseModel<List<LookupDTO>>();
            try
            {
                IQueryable<LookupDTO> categoriesDTO = categoryRepo.GetAllCategories();

                returnResponse.Entity = categoriesDTO.ToList();
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            return returnResponse;
        }


        public async Task<ResponseModel<ProductDTO>> GetProductDetails(int productId) {

            ResponseModel<ProductDTO> returnResponse = new ResponseModel<ProductDTO>();
            try
            {
                Task<ProductDTO> productDTO = productRepo.GetProductInfo(productId);

                returnResponse.Entity = await productDTO;
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
        #endregion
    }
}
