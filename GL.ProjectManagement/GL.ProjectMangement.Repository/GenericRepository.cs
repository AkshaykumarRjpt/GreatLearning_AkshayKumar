using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectMangement.Repository
{
    public class GenericRepository<T>
        : IRepository<T> where T : EntityBase
    {
        protected ProjectManagementDBContext context;

        public GenericRepository(ProjectManagementDBContext context)
        {
            this.context = context;
        }

        public virtual T Add(T entity)
        {
            return context
                .Add(entity)
                .Entity;
        }

        public virtual T Get(string id)
        {
            return context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return context.Set<T>()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity)
                .Entity;
        }

        

        public virtual T Delete(string id)
        {
            return context.Remove(context.Find<T>(id)).Entity;
        }

        async System.Threading.Tasks.Task IRepository<T>.SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
