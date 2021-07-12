using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.API.Services;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.Domain.Services;
using ShoppingOnline.DTO;
using System;
using System.Threading.Tasks;

namespace ShoppingOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }



        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            try
            {
                ResponseModel<User> registerResponse = new ResponseModel<User>();
                registerResponse = await _userService.RegisterUser(registerDto);
                if (!registerResponse.ReturnStatus)
                    return BadRequest(registerResponse);

                ResponseModel<UserDTO> registerResponseDto = new ResponseModel<UserDTO>()
                {
                    Entity = new UserDTO()
                    {
                        Token = await _tokenService.CreateToken(registerResponse.Entity),
                        UserName = registerDto.userName
                    },
                    ReturnStatus = true
                };

                return Ok(registerResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDto loginDto)
        {
            try
            {
                ResponseModel<User> loginResponse = new ResponseModel<User>();
                loginResponse = await _userService.LoginUser(loginDto);

                if (!loginResponse.ReturnStatus)
                    return BadRequest(loginResponse);

                ResponseModel<UserDTO> userResponseDto = new ResponseModel<UserDTO>()
                {
                    Entity = new UserDTO()
                    {
                        Token = await _tokenService.CreateToken(loginResponse.Entity),
                        UserName = loginDto.userName
                    },
                    ReturnStatus = true
                };

                return Ok(userResponseDto);
            }
            catch (Exception)
            {
                return BadRequest("Something wrong happened!. Please try again later.");
            }
        }
    }
}
