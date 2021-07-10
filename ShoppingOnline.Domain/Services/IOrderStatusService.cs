using ShoppingOnline.Common.Models;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingOnline.Domain.Services
{
    public interface IOrderStatusService
    {
        ResponseModel<List<LookupDTO>> GetOrderStatusLookup();
    }
}
