using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingOnline.Domain.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.API.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _config;
        private readonly UserManager<User> userManager;

        public TokenService(IConfiguration config,UserManager<User> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _config = config;
            this.userManager = userManager;
        }
        public async Task<string> CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };

            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials= creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }


        public string GetToken() {
            return _config["TokenKey"];
        }
    }
}
