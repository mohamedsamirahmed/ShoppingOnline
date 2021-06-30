using ShoppingOnline.Common.Models;
using ShoppingOnline.DTO;
using System.Threading.Tasks;

namespace ShoppingOnline.Domain.Services
{
    public interface IUserService
    {
        ResponseModel<RegisterDTO> RegisterUser(string username, string password);
        ResponseModel<LoginDto> LoginUser(string username, string password);
    }
}
