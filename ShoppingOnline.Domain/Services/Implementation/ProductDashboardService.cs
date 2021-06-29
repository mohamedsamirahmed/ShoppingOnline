using AutoMapper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.DTO;
using System;
using System.Linq;

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
        
        #endregion
        
        #region Constructor
        public ProductDashboardService(ShoppingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region public Operations

        public ResponseModel<IQueryable<ProductDTO>> GetAllProducts()
        {
            ResponseModel<IQueryable<ProductDTO>> returnResponse = new ResponseModel<IQueryable<ProductDTO>>();

            try
            {
                 IQueryable<ProductDTO> products = productRepo.GetAll().Select(p => new ProductDTO() { Id = p.Id,Name = p.Name,Description=p.Description,Price=p.Price });
                 returnResponse.Entity = products;
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
