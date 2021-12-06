using GL.ProjectManagement.Domain.Data;
using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectManagement.Domain.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ProjectManagementDBContext ProjectManagementDBContext;

        public ProjectRepository(ProjectManagementDBContext projectManagementDBContext)
            :base(projectManagementDBContext)
        {
            ProjectManagementDBContext = projectManagementDBContext;
        }

        public async Task<string> CreateProjectAsync(Project project)
        {
            try
            {
                await AddAsync(project);
                return $"Project successfully created with id:{project.Id}";
            }
            catch (Exception e)
            {
                return $"Project could not be created with id:{project.Id}";
            }
        }

        public async Task<bool> DeleteProjectAsync(Project project)
        {
            try
            {
                await DeleteAsync(project);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> UpdateProjectAsync(Project project)
        {
            try
            {
                await UpdateAsync(project);
                return $"Project successfully Update with id:{project.Id}";
            }
            catch (Exception e)
            {
                return $"Project could not be Updated with id:{project.Id}";
            }
        }
    }
}
