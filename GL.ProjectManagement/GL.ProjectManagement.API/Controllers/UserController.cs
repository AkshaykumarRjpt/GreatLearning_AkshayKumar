using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/User")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching All Users");
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet]
        [Route("api/User/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            _logger.LogInformation($"Fetching user with id: {id}");
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound($"User not found with id {id}");
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User")]
        public async Task<IActionResult> Create(UserCreation user)
        {
            _logger.LogInformation("Creating new user", 
                user.FirstName, user.LastName, user.Email);

            return Ok(await _userService.CreateUser(user));
        }

        [HttpPut]
        [Route("api/User")]
        public async Task<IActionResult> Update(UserUpdate user)
        {
            _logger.LogInformation($"Updating user with {user.Id}");
            if (await _userService.UpdateUser(user) == null)
            {
                return NotFound($"User not found with id: {user.Id}");
            }
            return Ok($"Update successful for user with id: {user.Id}");
        }

        [HttpDelete]
        [Route("api/User/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation($"Deleting User with id: {id}");
            var isDeleteSuccessful = await _userService.DeleteUser(id);
            if (isDeleteSuccessful == false)
            {
                return NotFound($"User not found with {id}");
            }
            return Ok($"User with id : {id} deleted");
        }
    }
}
