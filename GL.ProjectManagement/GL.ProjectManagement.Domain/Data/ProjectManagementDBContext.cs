using GL.ProjectManagement.Domain.Entities;
using GL.ProjectManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GL.ProjectManagement.Domain.Data
{
    public class ProjectManagementDBContext : DbContext
    {
        public ProjectManagementDBContext(DbContextOptions<ProjectManagementDBContext> options)
            :base(options) 
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Task>().ToTable("Task");
            modelBuilder.Entity<User>().ToTable("User");

            //var userentitybuilder = modelBuilder.Entity<User>();
            //userentitybuilder.HasKey(x => x.Id);
            //userentitybuilder.OwnsOne(e => e.Email, 
            //    p => { p.Property(x => x.EmailAddress).IsRequired(); });


        }

        
    }
}
