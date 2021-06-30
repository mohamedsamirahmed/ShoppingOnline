using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Common.Models;
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

        
        [HttpGet("GetProducts")]
        [AllowAnonymous]
        public IActionResult GetProducts()
        {
            try
            {
                ResponseModel<IQueryable<ProductDTO>> productResponse = new ResponseModel<IQueryable<ProductDTO>>();
                productResponse = _productDashboardService.GetAllProducts();
                if (!productResponse.ReturnStatus)
                    return BadRequest(productResponse);
                return Ok(new { data= productResponse.Entity,status=productResponse.ReturnStatus});
            }
            catch (Exception)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

        
        // GET api/<ProductDashboardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductDashboardController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductDashboardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductDashboardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
