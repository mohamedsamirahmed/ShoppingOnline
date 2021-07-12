using Microsoft.AspNetCore.Identity;
using ShoppingOnline.Common.Models;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services
{
    public interface IUserService
    {
        Task<ResponseModel<User>> RegisterUser(RegisterDTO registerDto);
        Task<ResponseModel<User>> LoginUser(LoginDto loginDto);
        Task<User> UserExists(string userName);
    }
}
