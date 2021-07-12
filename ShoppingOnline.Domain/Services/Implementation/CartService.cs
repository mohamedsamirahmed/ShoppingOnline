using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.Domain.Repositories.Implementation;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services.Implementation
{
    public class CartService : ICartService
    {
        #region Property Declaration
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ShoppingOnlineDBContext _dbContext;
        #endregion

        #region Repositories Property
        public ICartRepository cartRepo
        {
            get
            {
                if (_cartRepo == null)
                {
                    _cartRepo = new CartRepository(_dbContext, _mapper);
                }
                return _cartRepo;
            }
        }

        private ICartRepository _cartRepo;

        public ICartStatusRepository cartStatusRepo
        {
            get
            {
                if (_cartStatusRepo == null)
                {
                    _cartStatusRepo = new CartStatusRepository(_dbContext);
                }
                return _cartStatusRepo;
            }
        }
        private ICartStatusRepository _cartStatusRepo;

        public IOrderStatusRepository orderStatusRepo
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


        public ICartItemRepository cartItemRepo
        {
            get
            {
                if (_cartItemRepo == null)
                {
                    _cartItemRepo = new CartItemRepository(_dbContext, _mapper);
                }
                return _cartItemRepo;
            }
        }

        private ICartItemRepository _cartItemRepo;

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

        public IOrderItemRepository orderItemRepo
        {
            get
            {
                if (_orderItemRepo == null)
                {
                    _orderItemRepo = new OrderItemRepository(_dbContext, _mapper);
                }
                return _orderItemRepo;
            }
        }

        private IOrderItemRepository _orderItemRepo;

        #endregion

        public CartService(IUserService userService, IMapper mapper,
            ShoppingOnlineDBContext dbContext)
        {
            _mapper = mapper;
            _userService = userService;
            _dbContext = dbContext;
        }


        public async Task<ResponseModel<ProductDTO>> AddCartItem(ProductDTO productDto, string currentUserName)
        {
            ResponseModel<ProductDTO> cartItemResponse = new ResponseModel<ProductDTO>();

            try
            {
                var currentUser = await _userService.UserExists(currentUserName);
                if (currentUser == null)
                {
                    cartItemResponse.ReturnStatus = false;
                    cartItemResponse.ReturnMessage.Add("User not exist!");
                    return cartItemResponse;
                }

                Cart currentCart = await cartRepo.ActiveCartExist(currentUser.Id);

                int cartId = 0;
                if (currentCart == null)
                {
                    var NewCartStatus = await cartStatusRepo.GetAll().SingleOrDefaultAsync(x => x.Name == "New");

                    if (NewCartStatus == null)
                    {
                        cartItemResponse.ReturnStatus = false;
                        cartItemResponse.ReturnMessage.Add("Something went wrong!");
                        return cartItemResponse;
                    }

                    cartId = await cartRepo.AddCart(NewCartStatus.Id, currentUser.Id);
                }
                else
                    cartId = currentCart.Id;

                if (cartId == 0)
                {
                    cartItemResponse.ReturnStatus = false;
                    cartItemResponse.ReturnMessage.Add("Something went wrong!");
                    return cartItemResponse;
                }

                var cartItem = _mapper.Map<CartItem>(productDto);

                cartItem.CartId = cartId;

                cartItemRepo.Add(cartItem);
                await cartItemRepo.SaveChangesAsync();

                cartItemResponse.Entity = productDto;
                cartItemResponse.ReturnStatus = true;

                return cartItemResponse;

            }
            catch (Exception ex)
            {
                cartItemResponse.ReturnStatus = false;
                cartItemResponse.ReturnMessage.Add(ex.Message);
                return cartItemResponse;
            }
        }


        public async Task<ResponseModel<IQueryable<CartItemDTO>>> GetAllCartItems(string currentUserName)
        {
            ResponseModel<IQueryable<CartItemDTO>> cartItemResponse = new ResponseModel<IQueryable<CartItemDTO>>();

            try
            {
                var currentUser = await _userService.UserExists(currentUserName);
                if (currentUser == null)
                {
                    cartItemResponse.ReturnStatus = false;
                    cartItemResponse.ReturnMessage.Add("User not exist!");
                    return cartItemResponse;
                }

                var cartItems = cartItemRepo.GetCartItems(currentUser.Id);
                cartItemResponse.Entity = cartItems;
                cartItemResponse.ReturnStatus = true;
                return cartItemResponse;
            }
            catch (Exception ex)
            {
                cartItemResponse.ReturnMessage.Add(ex.Message);
                cartItemResponse.ReturnStatus = false;
                return cartItemResponse;
            }
        }


        public async Task<int> DeleteCartItem(CartItemDTO cartItemDto)
        {
            try
            {
                return await cartItemRepo.DeleteCartItem(cartItemDto);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<ResponseModel<int>> CheckoutCartProcess(string currentUserName, string shipmentAddress)
        {
            ResponseModel<int> orderResponse = new ResponseModel<int>();

            try
            {
                var currentUser = await _userService.UserExists(currentUserName);
                if (currentUser == null)
                {
                    orderResponse.ReturnStatus = false;
                    orderResponse.ReturnMessage.Add("User not exist!");
                    return orderResponse;
                }

                Order currentOrder = await orderRepo.ActiveOrderExist(currentUser.Id);
                var currentCart = await cartRepo.GetAll().Include(c => c.CartStatus)
                      .SingleOrDefaultAsync(c => c.UserId == currentUser.Id && c.CartStatus.Name == "New");
                var cartItems = cartItemRepo.GetAll().Where(c => c.CartId == currentCart.Id);

                int orderId = 0;
                if (currentOrder == null)
                {
                    var allOrderStatus = orderStatusRepo.GetAll();
                    //update OrderStatus to Checkedout
                    var CheckedoutOrderStatus = await allOrderStatus.SingleOrDefaultAsync(o => o.Name == "Checkedout");
                    if (CheckedoutOrderStatus == null)
                    {
                        orderResponse.ReturnStatus = false;
                        orderResponse.ReturnMessage.Add("Something went wrong!");
                        return orderResponse;
                    }

                    var order = new Order()
                    {
                        OrderStatusId = CheckedoutOrderStatus.Id,
                        ShipmentAddress = shipmentAddress,
                        TotalPrice = CalculateOrderPrice(cartItems)
                    };

                    //create new order
                    orderRepo.Add(order);
                    orderId = await orderRepo.SaveChangesAsync();

                    //Add Orders to Order Items
                    await AddOrderItems(cartItems, currentOrder.Id);
                    
                    //update cart status to checkedout
                    var cartId = await CheckoutCart(currentCart);
                }
                else
                    orderId = currentOrder.Id;

                if (orderId == 0)
                {
                    orderResponse.ReturnStatus = false;
                    orderResponse.ReturnMessage.Add("Something went wrong!");
                    return orderResponse;
                }
                
                orderResponse.Entity = orderId;
                orderResponse.ReturnStatus = true;

                return orderResponse;
            }
            catch (Exception ex)
            {
                orderResponse.ReturnStatus = false;
                orderResponse.ReturnMessage.Add(ex.Message);
                return orderResponse;
            }
        }


        public async Task<ResponseModel<IQueryable<OrderItemDTO>>> GetAllOrderItems(string currentUserName)
        {
            ResponseModel<IQueryable<OrderItemDTO>> cartItemResponse = new ResponseModel<IQueryable<OrderItemDTO>>();

            try
            {
                var currentUser = await _userService.UserExists(currentUserName);
                if (currentUser == null)
                {
                    cartItemResponse.ReturnStatus = false;
                    cartItemResponse.ReturnMessage.Add("User not exist!");
                    return cartItemResponse;
                }

                var orderItems = orderItemRepo.GetOrderItems(currentUser.Id);
                cartItemResponse.Entity = orderItems;
                cartItemResponse.ReturnStatus = true;
                return cartItemResponse;
            }
            catch (Exception ex)
            {
                cartItemResponse.ReturnMessage.Add(ex.Message);
                cartItemResponse.ReturnStatus = false;
                return cartItemResponse;
            }
        }

        #region helpers
        /// <summary>
        /// add cart items to order item table 
        /// </summary>
        /// <param name="cartItems"> current items on cart</param>
        /// <param name="currentOrderId">current order id</param>
        /// <returns></returns>
        private async Task AddOrderItems(IQueryable<CartItem> cartItems, int currentOrderId)
        {
            try
            {
                var orderItems = _mapper.Map<ICollection<OrderItems>>(cartItems);

                foreach (var orderItem in orderItems)
                {
                    orderItem.OrderId = currentOrderId;

                    orderItemRepo.Add(orderItem);
                }
                await orderItemRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private async Task<int> CreateNewOrder(int orderStatusId, int currentUserId)
        {
            try
            {
                //Add Order Record with status new
                return await orderRepo.AddOrder(orderStatusId, currentUserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// set cart status checkedout
        /// </summary>
        /// <param name="currentCart">current cart for curent user</param>
        /// <returns></returns>
        private async Task<int> CheckoutCart(Cart currentCart)
        {
            try
            {
                var allCartStatus = cartStatusRepo.GetAll();
                var CheckedoutCartStatus = await allCartStatus.SingleOrDefaultAsync(c => c.Name == "Checkedout");
                currentCart.CartStatusId = CheckedoutCartStatus.Id;
                cartRepo.Update(currentCart);
                return await cartRepo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// sum all cart item prices
        /// </summary>
        /// <param name="cartItems">current items on user's cart</param>
        /// <returns></returns>
        private double CalculateOrderPrice(IQueryable<CartItem> cartItems)
        {
            try
            {
                double totalprice = 0;
                foreach (var cartItem in cartItems)
                {
                    totalprice = totalprice + cartItem.Price;
                }

                return totalprice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        #endregion

    }
}
