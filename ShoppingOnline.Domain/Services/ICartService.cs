using ShoppingOnline.Common.Models;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services
{
    public interface ICartService
    {
        Task<ResponseModel<ProductDTO>> AddCartItem(ProductDTO productDto, string currentUserName);
        Task<ResponseModel<IQueryable<CartItemDTO>>> GetAllCartItems(string currentUserName);
        Task<int> DeleteCartItem(CartItemDTO cartItemDto);
        Task<ResponseModel<int>> CheckoutCartProcess(string currentUserName,string shipmentAddress);

        Task<ResponseModel<IQueryable<OrderItemDTO>>> GetAllOrderItems(string currentUserName);
    }
}
