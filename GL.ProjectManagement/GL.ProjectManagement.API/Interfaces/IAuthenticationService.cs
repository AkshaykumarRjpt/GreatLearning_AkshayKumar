using GL.ProjectManagement.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(LoginCredentials loginCredentials);
    }
}
