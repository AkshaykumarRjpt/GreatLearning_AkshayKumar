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
                        Email = "Akshay@GL.com"
                    });

                context.Users.Add(
                    new User
                    {
                        FirstName = "Shubham",
                        LastName = "Kumar",
                        Password = "Shubham",
                        Id = 2,
                        Email = "Shubham@GL.com"
                    });

                context.Projects.Add(
                    new Project
                    {
                        Id = 1,
                        Name = "GL Project",
                        Detail = "Sprint1"
                    });
                context.Projects.Add(
                    new Project
                    {
                        Id = 2,
                        Name = "GL Project Dotnet",
                        Detail = "Dotnet",
                        CreatedOn = DateTime.Now
                    });
                context.Projects.Add(
                    new Project
                    {
                        Id = 3,
                        Name = "GL Project Azure",
                        Detail = "Azure",
                        CreatedOn = DateTime.Now.AddDays(-1)
                    });
                

                context.Tasks.Add(
                    new Task
                    {
                        Id = 1,
                        ProjectId = 1,
                        Detail = "Sprint 1",
                        Status = 0,
                        AssignedToUserId = 1,
                        CreatedOn = DateTime.Now

                    }
                    );
                context.Tasks.Add(
                    new Task
                    {
                        Id = 2,
                        ProjectId = 1,
                        Detail = "Sprint 2",
                        Status = 0,
                        AssignedToUserId = 1,
                        CreatedOn = DateTime.Now.AddDays(-3)
                    }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
