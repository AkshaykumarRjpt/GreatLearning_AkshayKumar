using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL.ProjectMangement.Repository
{
    public class ProjectRepository: GenericRepository<Project>
    { 
    public ProjectRepository(ProjectManagementDBContext context) 
        : base(context)
    {
    }

    public override Project Add(Project project)
    {
        var entity = context.Add(project).Entity;
        context.SaveChanges();
        return entity;
    }

    public override IEnumerable<Project> All()
    {
        return context.Projects.AsNoTracking().ToList();
    }

    public override Project Get(string id)
    {
        return context.Projects.Where(t => t.Id == int.Parse(id)).AsNoTracking().FirstOrDefault();
    }

    public override Project Update(Project entity)
    {
        var project = context.Projects.AsNoTracking()
            .FirstOrDefault(p => p.Id == entity.Id);
        if (project != null)
        {
            project.Name = entity.Name;
            project.Detail = entity.Detail;

            var updatedentity = context.Projects.Update(project).Entity;
            context.SaveChanges();
            return updatedentity;
        }
        else
        {
            return null;
        }
    }

    public override Project Delete(string id)
    {
        var project = context.Projects.AsNoTracking()
       .FirstOrDefault(p => p.Id == int.Parse(id));
        var entity = context.Projects.Remove(project).Entity;
        context.SaveChanges();
        return entity;
    }
}
}
