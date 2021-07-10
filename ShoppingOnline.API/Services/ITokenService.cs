using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.API.Services
{
   public interface ITokenService
    {
        Task<string> CreateToken(User user);
       string GetToken();
    }
}
