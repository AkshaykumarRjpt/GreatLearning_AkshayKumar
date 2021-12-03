using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Services
{
    public class JWTAuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly string _key;

        public JWTAuthenticationService(IUserService userService)
        {
            _userService = userService;
            _key = "AKSHAYKUMARRAJPUT";
        }
        public async Task<string> Authenticate(LoginCredentials loginCredentials)
        {
            var isValidUser = await _userService.isValidUser(loginCredentials);
            if (!isValidUser)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                      new Claim(ClaimTypes.Email, loginCredentials.Email)
                  }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
