using AutoMapper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.Domain.Repositories.Implementation;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingOnline.Domain.Services.Implementation
{
  
   public class OrderStatusService : IOrderStatusService
    {
        #region Property Declaration
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        #endregion

        #region Repositories Property
        public IOrderStatusRepository OrderStatusRepo
        {
            get
            {
                if (_orderStatusRepo == null)
                {
                    _orderStatusRepo = new OrderStatusRepository(_dbContext, _mapper);
                }
                return _orderStatusRepo;
            }
        }

        private IOrderStatusRepository _orderStatusRepo;

        #endregion

        #region Constructor
        public OrderStatusService(ShoppingOnlineDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion

        public ResponseModel<List<LookupDTO>> GetOrderStatusLookup()
        {
            ResponseModel<List<LookupDTO>> returnResponse = new ResponseModel<List<LookupDTO>>();
            try
            {
                IQueryable<LookupDTO> orderStatusLstDTO = OrderStatusRepo.GetAllOrderStatus();

                returnResponse.Entity = orderStatusLstDTO.ToList();
                returnResponse.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
            }

            return returnResponse;
        }

    }
}
