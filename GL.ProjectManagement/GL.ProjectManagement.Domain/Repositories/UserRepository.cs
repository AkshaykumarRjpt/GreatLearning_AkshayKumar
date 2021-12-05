using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectManagement.Domain.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProjectManagementDBContext projectManagementDBContext)
            :base(projectManagementDBContext)
        {

        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> CreateUserAsync(User user)
        {
            try
            {
                await AddAsync(user);
                return $"User successfully created with id:{user.Id}";
            }
            catch(Exception e)
            {
                return $"User could not be created with id:{user.Id}";
            }
        }

        public async Task<string> UpdateUserAsync(User user)
        {
            try
            {
                await UpdateAsync(user);
                return $"User successfully Update with id:{user.Id}";
            }
            catch (Exception e)
            {
                return $"User could not be Updated with id:{user.Id}";
            }
            
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            try
            {
                await DeleteAsync(user);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
