using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.API.Services
{
   public interface ITokenService
    {
       string CreateToken(string userName);
       string GetToken();
    }
}
