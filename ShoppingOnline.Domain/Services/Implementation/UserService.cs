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
        public UserService(ShoppingOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion


        #region public Operations
        public ResponseModel<RegisterDTO> RegisterUser(string userName, string password)
        {
            ResponseModel<RegisterDTO> returnResponse = new ResponseModel<RegisterDTO>();

            try
            {
                var selectedUser = UserExists(userName);

                if (selectedUser==null)
                {
                    using var hmac = new HMACSHA512();
                    var user = new User
                    {
                        Name = userName.ToLower(),
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                        PasswordSalt = hmac.Key
                    };

                    userRepo.Add(user);
                    userRepo.SaveChanges();

                    var _user = new RegisterDTO() { userName = user.Name };
                    returnResponse.Entity = _user;
                    returnResponse.ReturnStatus = true;
                }
                else
                {
                    var _user = new RegisterDTO() { userName = userName };
                    returnResponse.Entity = _user;
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

        private User UserExists(string userName)
        {
            try
            {
                User selectedUser = userRepo.GetAll().FirstOrDefault(u => u.Name == userName.ToLower());
              
                return selectedUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResponseModel<LoginDto> LoginUser(string userName, string password)
        {
            ResponseModel<LoginDto> returnResponse = new ResponseModel<LoginDto>();

            try
            {
                User requiredUser = UserExists(userName);
                
                if (requiredUser==null)
                {
                    returnResponse.Entity = null;
                    returnResponse.ReturnStatus = true;
                    returnResponse.ReturnMessage.Add("Un Authorized User!");
                    return returnResponse;
                }

                using var hmac = new HMACSHA512(requiredUser.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != requiredUser.PasswordHash[i])
                    {
                        returnResponse.Entity = null;
                        returnResponse.ReturnStatus = false;
                        returnResponse.ReturnMessage.Add("Un Authorized User!");
                        return returnResponse;
                    }
                }
                
                returnResponse.Entity = new LoginDto() { Name= requiredUser.Name};
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
