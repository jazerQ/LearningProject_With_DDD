using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public class JwtProvider
    {
        public async Task<string> GenerateToken(User user) 
        {
            using StreamReader stream = new StreamReader("securityKey.txt");
            string securityKey = await stream.ReadToEndAsync();
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey));

            var token = new JwtSecurityToken()
        }
    }
}
