using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services
{
   public interface IOrderService
    {
        Task<ResponseModel<PagedList<OrderDTO>>> GetAllOrders(OrderParams orderParams);
    }
}
