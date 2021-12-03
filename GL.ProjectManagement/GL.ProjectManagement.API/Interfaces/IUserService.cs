using GL.ProjectManagement.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserInfo>> GetAllUsers();
        public Task<UserInfo> GetUser(string id);
        public Task<string> CreateUser(UserCreation user);
        public Task<string> UpdateUser(UserUpdate updatedUser);
        public Task<bool> DeleteUser(string id);

        public Task<bool> isValidUser(LoginCredentials loginCredentials);
    }
}
