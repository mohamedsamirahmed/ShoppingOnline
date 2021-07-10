using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Common.Helper;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Domain.Helpers;
using ShoppingOnline.Domain.Services;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
        private IOrderStatusService _orderStatusService;
        private IOrderService _orderService;


        public AdminController(IOrderStatusService orderStatusService, IOrderService orderService)
        {
            _orderStatusService = orderStatusService;
            _orderService = orderService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("review-orders")]
        public async Task<IActionResult> ReviewOrders([FromQuery] OrderParams orderParams) {
            try
            {
                var orderResponse = await _orderService.GetAllOrders(orderParams);

                Response.AddPagination(orderResponse.Entity.CurrentPage, orderResponse.Entity.PageSize,
                   orderResponse.Entity.TotalCount, orderResponse.Entity.TotalPages);

                if (!orderResponse.ReturnStatus)
                    return BadRequest(orderResponse);
                return Ok(orderResponse);
            }
            catch (Exception)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("review-orders/{id}")]
        public IActionResult ReviewOrders(int id,[FromQuery] string orders)
        {
            
            return Ok(orders);
        }

        [HttpPost("edit-orders/{id}")]
        public IActionResult EditOrders(int id, [FromQuery] string orders) {
            var selectedOrders = orders.Split(',').ToArray();
            //find orders where selectedorder
            return Ok("order object");
        }

        // GET api/<ProductDashboardController>/5
        [HttpGet("GetOrderStatus")]
        //[Authorize(Policy = "PurchaseOrder")]
        public IActionResult GetOrderStatus()
        {
            try
            {
                ResponseModel<List<LookupDTO>> orderStatusResponse = new ResponseModel<List<LookupDTO>>();
                orderStatusResponse = _orderStatusService.GetOrderStatusLookup();
                if (!orderStatusResponse.ReturnStatus)
                    return BadRequest(orderStatusResponse);
                return Ok(orderStatusResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }
    }
}
