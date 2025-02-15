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
        public string GenerateToken(User user) 
        {
            Fi file = new File();
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey());

            var token = new JwtSecurityToken()
        }
    }
}
