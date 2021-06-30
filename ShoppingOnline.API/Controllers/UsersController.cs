using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.API.Services;
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
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService,ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDto)
        {
            try
            {
                ResponseModel<RegisterDTO> registerResponse = new ResponseModel<RegisterDTO>();
                registerResponse = _userService.RegisterUser(registerDto.Name, registerDto.Password);
                if (!registerResponse.ReturnStatus)
                    return BadRequest(registerResponse);

                ResponseModel<UserDTO> userResponse = new ResponseModel<UserDTO>();
                userResponse.Entity = new UserDTO { UserName = registerResponse.Entity.Name, Token = _tokenService.CreateToken(registerResponse.Entity.Name) };
                userResponse.ReturnStatus = true;

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

        [HttpPost("login")]
        public IActionResult login(LoginDto loginDto)
        {
            try
            {
                ResponseModel<LoginDto> loginResponse = new ResponseModel<LoginDto>();
                loginResponse = _userService.LoginUser(loginDto.Name, loginDto.Password);
                if (!loginResponse.ReturnStatus)
                    return BadRequest(loginResponse);

                ResponseModel<UserDTO> userResponse = new ResponseModel<UserDTO>();
                userResponse.Entity = new UserDTO { UserName = loginResponse.Entity.Name, Token =_tokenService.GetToken()};
                userResponse.ReturnStatus = true;

                return Ok(userResponse);
            }
            catch (Exception)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
