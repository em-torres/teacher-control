using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildUserModel
    {
        public static ModelBuilder BuildUser(this ModelBuilder builder)
        {
            EntityTypeBuilder<User> model = builder.Entity<User>();
            

            model.Property(i => i.AuthToken).IsRequired().HasMaxLength(50).HasValueGenerator(typeof(TokenGuidGenerator)).ValueGeneratedOnAdd();
            model.HasOne(i => i.UserInfo);

            model.HasIndex(i => i.AuthToken).IsUnique();

            return builder;
        }

        public static ModelBuilder BuildUserCourse(this ModelBuilder builder)
        {
            EntityTypeBuilder<UserCourse> model = builder.Entity<UserCourse>();
            model.HasKey(i => new { i.CourseId, i.UserId });

            model
                .HasOne(i => i.User)
                .WithMany(i => i.Courses)
                .HasForeignKey(i => i.CourseId);

            model
                .HasOne(i => i.Course)
                .WithMany(i => i.Professors)
                .HasForeignKey(i => i.UserId);

            return builder;
        }

        public static ModelBuilder BuildUserGroup(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasKey(t => new { t.GroupId, t.UserId });

            modelBuilder.Entity<UserGroup>().HasOne(i => i.Group).WithMany(i => i.Users).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserGroup>().HasOne(i => i.User).WithMany(i => i.Groups).OnDelete(DeleteBehavior.Cascade);

            return modelBuilder;
        }
    }
}
