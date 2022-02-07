using GL.ProjectManagement.API.DTO;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectMangement.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GL.ProjectManagement.API.Services
{
    public class JWTAuthenticationService : IAuthenticationService
    {
        private readonly string _key;
        private readonly IRepository<User> repo;

        public JWTAuthenticationService(IRepository<User> repo)
        {            
            _key = "AKSHAYKUMARRAJPUT";
            this.repo = repo;
        }
        public LoginResponseDTO Authenticate(LoginCredentials loginCredentials)
        {
            var User = repo.All().FirstOrDefault(u => u.Email.ToLower() == loginCredentials.Email.ToLower() && u.Password == loginCredentials.Password);
            if (User == null)
            {
                return null;
            }
            var response = new LoginResponseDTO() { CurrentUser = User };

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
            response.Token = tokenHandler.WriteToken(token);
            return response;
        }
    }
}
