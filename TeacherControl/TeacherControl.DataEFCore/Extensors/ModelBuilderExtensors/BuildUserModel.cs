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

            model.Property(i => i.Password).IsRequired().HasMaxLength(100);
            model.Property(i => i.SaltToken).IsRequired().HasMaxLength(32).HasValueGenerator(typeof(TokenGuidGenerator)).ValueGeneratedOnAdd();
            model.Property(i => i.StatusId).IsRequired();
            model.HasIndex(i => i.StatusId).IsUnique(false);

            model.HasIndex(i => i.SaltToken).IsUnique();

            return builder;
        }

        public static ModelBuilder BuildUserInfo(this ModelBuilder builder)
        {
            EntityTypeBuilder<UserInfo> model = builder.Entity<UserInfo>();

            model.Property(i => i.FirstName).IsRequired().HasMaxLength(100);
            model.Property(i => i.FirstName).IsRequired().HasMaxLength(100);
            model.Property(i => i.Email).IsRequired().HasMaxLength(60);

            model.HasIndex(i => i.Email).IsUnique();

            model.HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<UserInfo>(i => i.UserId);

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

            modelBuilder.Entity<UserGroup>().HasOne(i => i.Group).WithMany(i => i.Users);
            modelBuilder.Entity<UserGroup>().HasOne(i => i.User).WithMany(i => i.Groups);

            return modelBuilder;
        }
    }
}
