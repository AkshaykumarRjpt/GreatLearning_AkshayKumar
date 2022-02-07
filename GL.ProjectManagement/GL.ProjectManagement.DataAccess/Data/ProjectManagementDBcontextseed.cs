using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GL.ProjectManagement.Domain.Data
{
    public static class ProjectManagementDBcontextseed
    {
        public static async System.Threading.Tasks.Task SeedDataAsync(ProjectManagementDBContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(
                    new User
                    {
                        FirstName = "Akshay",
                        LastName = "Kumar",
                        Password = "Akshay",
                        Id = 1,
                        Email = "Ak@GL.com"
                    }
                ); ;

                context.Projects.Add(
                    new Project
                    {
                        Id = 1,
                        Name = "GL Project",
                        Detail = "Sprint2"
                    }
                );

                context.Tasks.Add(
                    new Task
                    {
                        Id = 1,
                        ProjectId = 1,
                        Detail = "some detail",
                        Status = 0,
                        AssignedToUserId = 1
                    }
                    );
                context.Tasks.Add(
                    new Task
                    {
                        Id = 2,
                        ProjectId = 1,
                        Detail = "some detail",
                        Status = 0,
                        AssignedToUserId = 1
                    }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
