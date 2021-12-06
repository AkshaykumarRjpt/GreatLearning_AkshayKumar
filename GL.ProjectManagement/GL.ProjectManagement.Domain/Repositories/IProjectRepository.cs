using GL.ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectManagement.Domain.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<List<Project>> GetAllProjectsAsync();
        Task<string> CreateProjectAsync(Project project);
        Task<string> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(Project project);
    }
}
