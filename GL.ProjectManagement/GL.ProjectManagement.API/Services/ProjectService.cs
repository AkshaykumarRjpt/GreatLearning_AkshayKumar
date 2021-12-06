using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Services
{
    public class ProjectService : IProjectService
    {        
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public async Task<List<ProjectInfo>> GetAllProjects()
        {
            var Projects = await projectRepository.GetAllProjectsAsync();
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var result = new List<ProjectInfo>();
                foreach (var project in Projects)
                {
                    result.Add(new ProjectInfo { 
                        Id = project.Id.ToString(), 
                        Detail = project.Detail, 
                        Name = project.Name, 
                        CreatedOn = project.CreatedOn.ToString() });
                }
                return result;
            });
        }

        public async Task<ProjectInfo> GetProject(string id)
        {
            return await System.Threading.Tasks.Task.Run(async () =>
            {
                var res = await projectRepository.GetProjectByIdAsync(int.Parse(id));
                if (res == null)
                {
                    return null;
                }
                return new ProjectInfo { Id = res.Id.ToString(), 
                    Detail = res.Detail, 
                    Name = res.Name, 
                    CreatedOn = res.CreatedOn.ToString() };
            });
        }
        public async Task<string> CreateProject(ProjectCreation newProject)
        {            
            var project = new Project {                 
                Detail = newProject.Detail, 
                Name = newProject.Name, 
                CreatedOn = DateTime.Now };
            await System.Threading.Tasks.Task.Run(() => {

                projectRepository.CreateProjectAsync(project);

            });
            return $"Project created successfully with id: {project.Id}";
        }

        public async Task<bool> DeleteProject(string id)
        {
            var project = await projectRepository.GetProjectByIdAsync(int.Parse(id));
            return await System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    projectRepository.DeleteProjectAsync(project);
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
            var project = await projectRepository.GetProjectByIdAsync(int.Parse(updatedProject.Id));
            if(project != null)
            {
                project.Name = updatedProject.Name;
                project.Detail = updatedProject.Detail;
                project.CreatedOn = DateTime.Now;
            }
            return await System.Threading.Tasks.Task.Run(async () =>
            {
                return await projectRepository.UpdateProjectAsync(project);                
            });
        }
    }
}
