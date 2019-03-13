using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildCourseModel
    {
        public static ModelBuilder BuildCourse(this ModelBuilder builder)
        {
            EntityTypeBuilder<Course> model = builder.Entity<Course>();

            model.HasKey(b => b.Id);

            model.Property(b => b.Name).IsRequired().HasMaxLength(150);
            model.Property(b => b.CodeIdentifier).IsRequired().HasMaxLength(15);
            model.Property(b => b.Description).IsRequired(); //TODO: max length TBD
            model.Property(b => b.Credits).IsRequired();
            model.Property(b => b.StartDate).IsRequired();
            model.Property(b => b.EndDate).IsRequired();
            model.Property(b => b.Credits).IsRequired();

            model.HasIndex(b => b.StatusId).IsUnique(false);

            model.HasOne(b => b.Status)
                .WithOne()
                .HasForeignKey<Course>(i => i.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            return builder;
        }

        public static ModelBuilder BuildCourseTag(this ModelBuilder builder)
        {
            EntityTypeBuilder<CourseTag> model = builder.Entity<CourseTag>();

            model.Property(b => b.Name).IsRequired().HasMaxLength(30);
            model.HasOne(b => b.Course).WithMany(b => b.Tags).HasForeignKey(b => b.CourseId);

            return builder;
        }

        public static ModelBuilder BuildCourseUserCredit(this ModelBuilder builder)
        {
            EntityTypeBuilder<CourseUserCredit> model = builder.Entity<CourseUserCredit>();

            model.Property(b => b.Credits).IsRequired();

            model.HasOne(b => b.Student);
            model.HasOne(b => b.Course);

            return builder;
        }

        public static ModelBuilder BuildCourseComment(this ModelBuilder builder)
        {
            EntityTypeBuilder<CourseComment> model = builder.Entity<CourseComment>();

            model.Property(b => b.Title).IsRequired();
            model.Property(b => b.Upvote).HasDefaultValue(0).IsRequired();
            model.Property(b => b.Body).IsRequired();
            model.Property(b => b.Body).IsRequired();

            model.HasIndex(b => b.StatusId).IsUnique(false);

            model.HasOne(b => b.Author);
            model.HasOne(b => b.Course);

            model.HasOne(b => b.Status)
                .WithOne()
                .HasForeignKey<CourseComment>(b => b.StatusId).OnDelete(DeleteBehavior.Restrict);

            model.HasOne(b => b.Course).WithMany(b => b.Comments).HasForeignKey(b => b.CourseId);

            return builder;

        }
    }
}
