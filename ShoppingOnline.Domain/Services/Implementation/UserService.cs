using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.Domain.Repositories;
using ShoppingOnline.Domain.Repositories.Implementation;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        #region Property Declaration
        private readonly ShoppingOnlineDBContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        #endregion

        #region Repositories Property
        public IUserRepository userRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(_dbContext);
                }
                return _userRepo;
            }
        }

        private IUserRepository _userRepo;

        #endregion

        #region Constructor
        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        #endregion


        #region public Operations
        public async Task<ResponseModel<User>> RegisterUser(RegisterDTO registerDto)
        {
            ResponseModel<User> returnResponse = new ResponseModel<User>();
            try
            {
                var selectedUser = await UserExists(registerDto.userName);

                if (selectedUser == null)
                {
                    var userFromRegisterDTO = _mapper.Map<User>(registerDto);

                    userFromRegisterDTO.UserName = registerDto.userName.ToLower();
                    var userDTO = _mapper.Map<User>(userFromRegisterDTO);

                    var userResult = await _userManager.CreateAsync(userFromRegisterDTO, registerDto.Password);
                    
                    if (!userResult.Succeeded)
                        returnResponse.Entity = null;
                    selectedUser = await _userManager.FindByNameAsync(userFromRegisterDTO.UserName);

                    var roleResult = await _userManager.AddToRoleAsync(selectedUser, "Member");

                    if (!userResult.Succeeded)
                        returnResponse.Entity = null;


                    returnResponse.Entity = userDTO;
                    returnResponse.ReturnStatus = true;
                }
                else
                {
                    returnResponse.Entity = null;
                    returnResponse.ReturnStatus = false;
                    returnResponse.ReturnMessage.Add("User Exists!");
                }
                return returnResponse;
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return returnResponse;
            }
        }

        public async Task<User> UserExists(string userName)
        {
            try
            {
                User selectedUser = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == userName.ToLower());

                return selectedUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<User>> LoginUser(LoginDto loginDto)
        {
            ResponseModel<User> returnResponse = new ResponseModel<User>();
            try
            {
                User requiredUser = await UserExists(loginDto.userName);

                if (requiredUser == null)
                {
                    returnResponse.Entity = null;
                    returnResponse.ReturnStatus = true;
                    returnResponse.ReturnMessage.Add("Un Authorized User!");
                    return returnResponse;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(requiredUser, loginDto.Password, false);
                if (!result.Succeeded)
                {
                    returnResponse.ReturnMessage.Add("UnAuthorized");
                    returnResponse.ReturnStatus = false;
                }

                //var userDto= _mapper.Map<User>(requiredUser);
                returnResponse.Entity = requiredUser;
                returnResponse.ReturnStatus = true;

                return returnResponse;
            }
            catch (Exception ex)
            {
                returnResponse.Entity = null;
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return returnResponse;
            }
        }
        #endregion
    }
}
