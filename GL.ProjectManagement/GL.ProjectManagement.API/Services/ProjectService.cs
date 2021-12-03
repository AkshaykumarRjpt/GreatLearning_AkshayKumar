using GL.ProjectManagement.API.DTOs;
using GL.ProjectManagement.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.Services
{
    public class ProjectService : IProjectService
    {
        public Task<string> CreateProject(ProjectCreation newProject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProject(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectInfo>> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectInfo> GetProject(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateProject(ProjectUpdate updatedProject)
        {
            throw new NotImplementedException();
        }
    }
}
