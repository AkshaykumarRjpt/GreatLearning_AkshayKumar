using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Detail)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(t => t.CreatedOn)
                .IsRequired();

            builder.HasMany(a => a.Task)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.ProjectId);
        }
    }
}
