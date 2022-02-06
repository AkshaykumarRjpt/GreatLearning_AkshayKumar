using GL.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.DTO
{
    public class LoginResponseDTO
    {
        public User CurrentUser { get; set; }
        public String Token { get; set; }
    }
}
