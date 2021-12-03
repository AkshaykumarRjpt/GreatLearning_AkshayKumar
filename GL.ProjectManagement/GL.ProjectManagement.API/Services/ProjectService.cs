using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Services
{
    public class ProjectService : IProjectService
    {
        private IList<Project> Projects;

        public ProjectService()
        {
            this.Projects = new List<Project>();
        }
        public async Task<List<ProjectInfo>> GetAllProjects()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var result = new List<ProjectInfo>();
                foreach (var project in Projects)
                {
                    result.Add(new ProjectInfo { Id = project.Id, Detail = project.Detail, Name = project.Name, CreatedOn = project.CreatedOn.ToString() });
                }
                return result;
            });
        }

        public async Task<ProjectInfo> GetProject(string id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var res = Projects.FirstOrDefault(p => p.Id == id);
                if (res == null)
                {
                    return null;
                }
                return new ProjectInfo { Id = res.Id, 
                    Detail = res.Detail, 
                    Name = res.Name, 
                    CreatedOn = res.CreatedOn.ToString() };
            });
        }
        public async Task<string> CreateProject(ProjectCreation newProject)
        {
            var count = this.Projects.Count;
            var project = new Project { 
                Id = count++.ToString(), 
                Detail = newProject.Detail, 
                Name = newProject.Name, 
                CreatedOn = DateTime.Now };
            await System.Threading.Tasks.Task.Run(() => {

                Projects.Add(project);

            });
            return $"Project created successfully with id: {project.Id}";
        }

        public async Task<bool> DeleteProject(string id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var ProjectToRemove = Projects.Single(x => x.Id == id);
                    Projects.Remove(ProjectToRemove);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }      

        public async Task<string> UpdateProject(ProjectUpdate updatedProject)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var project = Projects.FirstOrDefault(x => x.Id == updatedProject.Id);
                if (project != null)
                {
                    project.Name = updatedProject.Name;
                    project.Detail = updatedProject.Detail;
                    return project.Id;
                }
                else
                {
                    return null;
                }
            });
        }
    }
}
