using ArabDev.Data.Entities;
using ArabDev.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(User user, UserManager<User> userManager)
        {

            //payload
            //1.privareclaim[user-defined]
            var AuthClamis = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var UserRole = await userManager.GetRolesAsync(user);
            foreach (var Role in UserRole)
            {
                AuthClamis.Add(new Claim(ClaimTypes.Role,Role));    
            }

            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            if (key.Length < 32)
            {
                throw new ArgumentException("JWT Key must be at least 256 bits (32 bytes).");
            }


            var authKey = new SymmetricSecurityKey(key);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:VaildIssuer"],
                audience: _configuration["JWT:VaildAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClamis,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
