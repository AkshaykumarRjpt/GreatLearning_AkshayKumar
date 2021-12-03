using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.Exceptions;
using GL.ProjectManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Services
{
    public class UserService
    {
        private IList<User> Users;

        public UserService()
        {
            this.Users = new List<User>();
        }

        public async Task<List<UserInfo>> GetAllUsers()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var result = new List<UserInfo>();
                foreach (var user in Users)
                {
                    result.Add(new UserInfo 
                    { Id = user.Id, 
                        FirstName = user.FirstName, 
                        LastName = user.LastName, 
                        Email = user.Email });
                }
                return result;
            });
        }

        public async Task<UserInfo> GetUser(string id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var res = Users.FirstOrDefault(p => p.Id == id);
                if (res == null)
                {
                    return null;
                }
                return new UserInfo{ Id = res.Id, 
                    FirstName = res.FirstName, 
                    LastName = res.LastName, 
                    Email = res.Email };
            });
        }

        public async Task<string> CreateUser(UserCreation user)
        {
            var count = this.Users.Count;
            try
            {
                var newuser = new User { 
                    Id = count++.ToString(), 
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
                        Users.Add(newuser);
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
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var user = Users.FirstOrDefault(x => x.Id == updatedUser.Id);
                if (user != null)
                {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.Password = updatedUser.Password;
                    user.Email = Email.Address(updatedUser.Email);
                    return user.Id;
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<bool> DeleteUser(string id)
        {

            return await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var userToRemove = Users.Single(x => x.Id == id);
                    Users.Remove(userToRemove);
                    return true;
                }
                catch
                {
                    return false;
                }

            });
        }      

    }
}
