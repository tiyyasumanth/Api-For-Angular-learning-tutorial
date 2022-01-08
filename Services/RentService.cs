using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RentService : IRentService
    {
        private readonly string _key;
        public RentService(string key)
        {
            this._key = key;
        }
        public async Task<Token> GetTokenInfo(UserRequest userRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(this._key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userRequest.userName),
                    new Claim(ClaimTypes.Email,userRequest.emailGroup.emailAddress)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Token token1 = new Token();
            token1.access_Token = tokenHandler.WriteToken(token);
            token1.token_Expire_Time = "30";
            token1.status = true;
            return token1;
        }
    }
}