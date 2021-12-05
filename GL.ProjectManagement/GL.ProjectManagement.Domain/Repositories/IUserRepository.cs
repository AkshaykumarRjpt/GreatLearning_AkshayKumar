using GL.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectManagement.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<string> CreateUserAsync(User user);
        Task<string> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
    }
}
