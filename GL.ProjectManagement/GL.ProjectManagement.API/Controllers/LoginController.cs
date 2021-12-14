using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Controllers
{
    
    [ApiController]    
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenticationService _authManagerService;

        public LoginController(ILogger<LoginController> logger, IAuthenticationService authService)
        {
            _logger = logger;
            _authManagerService = authService;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(LoginCredentials loginCredentials)
        {
            _logger.LogInformation("Request to login");
            var token = _authManagerService.Authenticate(loginCredentials);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
    }
}
