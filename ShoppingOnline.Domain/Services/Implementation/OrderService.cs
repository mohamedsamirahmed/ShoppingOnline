using AutoMapper;
using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.Domain.Repositories.Implementation;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services.Implementation
{
    public class OrderService:IOrderService
    {
        #region Property Declaration
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly IMapper _mapper;
        #endregion

        #region Repositories Property
        public IOrderRepository orderRepo
        {
            get
            {
                if (_orderRepo == null)
                {
                    _orderRepo = new OrderRepository(_dbContext, _mapper);
                }
                return _orderRepo;
            }
        }

        private IOrderRepository _orderRepo;
        #endregion


        public OrderService(ShoppingOnlineDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PagedList<OrderDTO>>> GetAllOrders(OrderParams orderParams)
        {
            ResponseModel<PagedList<OrderDTO>> returnResponse = new ResponseModel<PagedList<OrderDTO>>();

            try
            {
                IQueryable<OrderDTO> ordersDTO = orderRepo.GetAllOrders();

                var pagedOrders = PagedList<OrderDTO>.CreateAsync(ordersDTO, orderParams.PageNumber, orderParams.PageSize);

                returnResponse.Entity = pagedOrders;
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
    }
}