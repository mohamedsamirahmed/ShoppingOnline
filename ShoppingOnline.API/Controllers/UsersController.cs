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

          

        // POST api/<UsersController>
        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDto)
         //public IActionResult Register(string username,string password)
        {
            try
            {
                ResponseModel<RegisterDTO> registerResponse = new ResponseModel<RegisterDTO>();
                registerResponse = _userService.RegisterUser(registerDto.userName, registerDto.Password);
                //registerResponse = _userService.RegisterUser(username, password);
                if (!registerResponse.ReturnStatus)
                    return BadRequest(registerResponse);

                ResponseModel<UserDTO> userResponse = new ResponseModel<UserDTO>();
                userResponse.Entity = new UserDTO { UserName = registerResponse.Entity.userName, Token = _tokenService.CreateToken(registerResponse.Entity.userName) };
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
                userResponse.Entity = new UserDTO { UserName = loginResponse.Entity.Name, Token =_tokenService.CreateToken(loginResponse.Entity.Name)};
                userResponse.ReturnStatus = true;

                return Ok(userResponse);
            }
            catch (Exception)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }
    }
}
