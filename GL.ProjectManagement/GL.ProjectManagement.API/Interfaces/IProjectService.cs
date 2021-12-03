using GL.ProjectManagement.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Interfaces
{
    public interface IProjectService
    {
        public Task<List<ProjectInfo>> GetAllProjects();
        public Task<ProjectInfo> GetProject(string id);
        public Task<string> CreateProject(ProjectCreation newProject);
        public Task<string> UpdateProject(ProjectUpdate updatedProject);        
        public Task<bool> DeleteProject(string id);
    }
}
