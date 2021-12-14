using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL.ProjectMangement.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ProjectManagementDBContext context) 
            : base(context)
        {

        }

        public override User Add(User user)
        {
            var entity = context.Add(user).Entity;
            context.SaveChanges();
            return entity;

        }

        public override IEnumerable<User> All()
        {
            return context.Users.AsNoTracking().ToList();
        }

        public override User Get(string id)
        {
            return context.Users.Where(t => t.Id == int.Parse(id)).AsNoTracking().FirstOrDefault();
        }

        public override User Update(User entity)
        {
            var user = context.Users.AsNoTracking()
                .FirstOrDefault(p => p.Id == entity.Id);
            if (user != null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Password = entity.Password;
                user.Email = Email.Address(entity.Email);
                var updatedentity = context.Users.Update(entity).Entity;
                context.SaveChanges();
                return updatedentity;
            }
            else
            {
                return null;
            }
        }

        public override User Delete(string id)
        {
            var user = context.Users.AsNoTracking()
            .FirstOrDefault(p => p.Id == int.Parse(id));
            var entity = context.Users.Remove(user).Entity;
            context.SaveChanges();
            return entity;
        }
    }
}
