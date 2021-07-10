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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDashboardController : ControllerBase
    {

        private IProductDashboardService _productDashboardService;


        public ProductDashboardController(IProductDashboardService productDashboardService)
        {
            _productDashboardService = productDashboardService;
        }


       // [Authorize(Policy = "PurchaseOrder")]
        [HttpGet("GetProducts")]
        
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams)
        {
            try
            {
                 var productResponse =await _productDashboardService.GetAllProducts(productParams);
                
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
    }
}
