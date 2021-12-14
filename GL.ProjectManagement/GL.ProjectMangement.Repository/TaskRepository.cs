using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL.ProjectMangement.Repository
{
    public class TaskRepository : GenericRepository<Task>
    {
        public TaskRepository(ProjectManagementDBContext context)
            : base(context)
        {
        }

        public override Task Add(Task task)
        {
            var entity = context.Add(task).Entity;
            context.SaveChanges();
            return entity;
        }

        public override IEnumerable<Task> All()
        {
            return context.Tasks.AsNoTracking().ToList();
        }

        public override Task Get(string id)
        {
            return context.Tasks.Where(t => t.Id == int.Parse(id)).AsNoTracking().FirstOrDefault();
        }

        public override Task Update(Task entity)
        {
            var task = context.Tasks.AsNoTracking()
                .FirstOrDefault(p => p.Id == entity.Id);
            if (task != null)
            {
                task.ProjectId = entity.ProjectId;
                task.Status = entity.Status;
                task.Detail = entity.Detail;
                task.AssignedToUserId = entity.AssignedToUserId;
                var updatedentity = context.Tasks.Update(task).Entity;
                context.SaveChanges();
                return updatedentity;
            }
            else
            {
                return null;
            }
        }

        public override Task Delete(string id)
        {
            var task = context.Tasks.AsNoTracking()
            .FirstOrDefault(p => p.Id == int.Parse(id));
            var entity = context.Tasks.Remove(task).Entity;
            context.SaveChanges();
            return entity;
        }
    }
}
