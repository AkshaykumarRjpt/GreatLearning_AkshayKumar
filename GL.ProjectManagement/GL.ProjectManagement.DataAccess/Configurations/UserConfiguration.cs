using GL.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasAlternateKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Email)
              .HasMaxLength(50)
              .IsRequired();

            builder.HasMany(t => t.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.AssignedToUserId);
        }
    }
}
