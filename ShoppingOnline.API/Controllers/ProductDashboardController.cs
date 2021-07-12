using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.API.Services;
using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.Domain.Services;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ShoppingOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDashboardController : ControllerBase
    {

        private IProductDashboardService _productDashboardService;
        private readonly ICartService _cartService;
        private readonly ITokenService _tokenService;

        public ProductDashboardController(IProductDashboardService productDashboardService,
            ICartService cartService, ITokenService tokenService)
        {
            _productDashboardService = productDashboardService;
            _cartService = cartService;
            _tokenService = tokenService;
        }


        // [Authorize(Policy = "PurchaseOrder")]
        [HttpGet("GetProducts")]

        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams)
        {
            try
            {
                var productResponse = await _productDashboardService.GetAllProducts(productParams);

                Response.AddPagination(productResponse.Entity.CurrentPage, productResponse.Entity.PageSize,
                   productResponse.Entity.TotalCount, productResponse.Entity.TotalPages);

                if (!productResponse.ReturnStatus)
                    return BadRequest(productResponse);
                //return Ok(new { data= productResponse.Entity,status=productResponse.ReturnStatus});
                return Ok(productResponse);
            }
            catch (Exception)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }


        // GET api/<ProductDashboardController>/5
        [HttpGet("GetCategories")]
        //[Authorize(Policy = "PurchaseOrder")]
        public IActionResult GetCategories()
        {
            try
            {
                ResponseModel<List<LookupDTO>> categoryResponse = new ResponseModel<List<LookupDTO>>();
                categoryResponse = _productDashboardService.GetCategoryLookup();
                if (!categoryResponse.ReturnStatus)
                    return BadRequest(categoryResponse);
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }


        // GET api/<ProductDashboardController>/5
        [HttpGet("GetProducts/{id}")]
        //[Authorize(Policy = "PurchaseOrder")]
        public async Task<IActionResult> GetProducts(int id)
        {
            try
            {
                var productResponse = await _productDashboardService.GetProductDetails(id);

                if (!productResponse.ReturnStatus)
                    return BadRequest(productResponse);
                return Ok(productResponse);

            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }


        [HttpPost("AddToCart/{username}")]
        public async Task<IActionResult> AddToCart(ProductDTO productDto, string userName)
        {
            try
            {

                var productDtoResponse = await _cartService.AddCartItem(productDto, userName);

                if (!productDtoResponse.ReturnStatus)
                    return BadRequest(productDtoResponse);
                return Ok(productDtoResponse);
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet("GetCartItems/{username}")]
        public async Task<IActionResult> GetCartItems(string username)
        {
            try
            {
                var cartItems = await _cartService.GetAllCartItems(username);
                return Ok(cartItems);
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("RemoveCartItem/")]
        public async Task<IActionResult> RemoveCartItem(CartItemDTO cartItemDto)
        {
            try
            {
                var res = await _cartService.DeleteCartItem(cartItemDto);
                if (res == 1)
                    return Ok();
                else
                    return BadRequest("Something Went wrong");
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }


        [HttpPost("CartCheckout/{username}/{shipmentAddress}")]
        public async Task<IActionResult> CartCheckout(string userName, string shipmentAddress)
        {
            try
            {
                var ordertResponse = await _cartService.CheckoutCartProcess(userName, shipmentAddress);

                if (!ordertResponse.ReturnStatus)
                    return BadRequest(ordertResponse);
                return Ok(ordertResponse);
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }



        [HttpGet("GetOrderItems/{username}")]
        public async Task<IActionResult> GetOrderItems(string username)
        {
            try
            {
                var orderItems = await _cartService.GetAllOrderItems(username);
                return Ok(orderItems);
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }

    }
}
