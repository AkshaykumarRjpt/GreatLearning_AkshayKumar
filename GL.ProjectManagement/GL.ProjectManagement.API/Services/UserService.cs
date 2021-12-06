using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.Exceptions;
using GL.ProjectManagement.Domain.ValueObjects;
using GL.ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<List<UserInfo>> GetAllUsers()
        {
            return await System.Threading.Tasks.Task.Run(async () =>
            {
                var result = new List<UserInfo>();
                var Users = await UserRepository.GetAllUsersAsync();
                foreach (var user in Users)
                {
                    result.Add(new UserInfo
                    {
                        Id = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    });
                }
                return result;
            });
          
        }

        public async Task<UserInfo> GetUser(string id)
        {
            return await System.Threading.Tasks.Task.Run(async () =>
            {
                var res = await UserRepository.GetUserByIdAsync(int.Parse(id));
                if (res == null)
                {
                    return null;
                }
                return new UserInfo
                {
                    Id = res.Id.ToString(),
                    FirstName = res.FirstName,
                    LastName = res.LastName,
                    Email = res.Email
                };
            });

           
        }

        public async Task<string> CreateUser(UserCreation user)
        {
            var Users = await UserRepository.GetAllUsersAsync();
            try
            {
                var newuser = new User {                      
                    FirstName = user.FirstName, 
                    LastName = user.LastName, 
                    Email = Email.Address(user.Email), 
                    Password = user.Password };

                await System.Threading.Tasks.Task.Run(() => {
                    if (Users.Any(t => t.Email.Equals(user.Email)))
                    {
                        throw new EmailAlreadyExist(user.Email);
                    }
                    else
                    {
                        UserRepository.CreateUserAsync(newuser);
                    }
                });
                return $"User successfully created with id:{newuser.Id}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateUser(UserUpdate updatedUser)
        {
            return await System.Threading.Tasks.Task.Run(async () =>
            {
                var user = await UserRepository.GetUserByIdAsync(int.Parse(updatedUser.Id));
                if (user != null)
                {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.Password = updatedUser.Password;
                    user.Email = Email.Address(updatedUser.Email);
                    return await UserRepository.UpdateUserAsync(user);
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<bool> DeleteUser(string id)
        {

            return await System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    var userToRemove = await UserRepository.GetUserByIdAsync(int.Parse(id));
                    await UserRepository.DeleteUserAsync(userToRemove);
                    
                    return true;
                }
                catch
                {
                    return false;
                }

            });
        }

        public async Task<bool> isValidUser(LoginCredentials loginCredentials)
        {
            var Users = await UserRepository.GetAllUsersAsync();
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var user = Users.FirstOrDefault(x => x.Email == loginCredentials.Email);
                if (user != null && user.Password == loginCredentials.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
