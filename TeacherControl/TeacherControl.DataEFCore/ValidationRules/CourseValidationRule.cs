using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Models;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class CourseValidationRule : BaseValidationRule
    {
        public CourseValidationRule(ModelBuilder builder) : base(builder)
        {

        }


        private void BuildCourseDbTable(EntityTypeBuilder<Course> model)
        {
            BuildBaseModelDbTable(model);
            model.HasKey(b => b.Id);

            model.Property(b => b.Name).IsRequired().HasMaxLength(150);
            model.Property(b => b.Description).IsRequired(); //TODO: max length TBD
            model.Property(b => b.Credits).IsRequired();
            model.Property(b => b.StartDate).IsRequired();
            model.Property(b => b.EndDate).IsRequired();
            model.Property(b => b.Credits).IsRequired();

            model.HasOne(b => b.Status).WithOne().HasForeignKey<Course>(b => b.StatusId).OnDelete(DeleteBehavior.Restrict);
            model.HasOne(b => b.Professor).WithOne().HasForeignKey<Course>(b => b.ProfessorId).OnDelete(DeleteBehavior.Restrict);

        }

        private void BuildCourseTagDbTable(EntityTypeBuilder<CourseTag> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Name).IsRequired().HasMaxLength(30);
            model.HasOne(b => b.Course).WithMany(b => b.Tags).HasForeignKey(b => b.CourseId);
        }

        private void BuildCourseUserCreditDbTable(EntityTypeBuilder<CourseUserCredit> model)
        {
            BuildBaseModelDbTable(model);
            model.Property(b => b.Credits).IsRequired();

            model.HasOne(b => b.Student);
            model.HasOne(b => b.Course);
        }

        private void BuildCourseCommentDbTable(EntityTypeBuilder<CourseComment> model)
        {
            BuildBaseModelDbTable(model);
            model.Property(b => b.Title).IsRequired();
            model.Property(b => b.Upvote).HasDefaultValue(0).IsRequired();
            model.Property(b => b.Body).IsRequired();
            model.Property(b => b.Body).IsRequired();

            model.HasOne(b => b.Author);
            model.HasOne(b => b.Course);

            model.HasOne(b => b.Status)
                .WithOne()
                .HasForeignKey<CourseComment>(b => b.StatusId).OnDelete(DeleteBehavior.Restrict);

            model.HasOne(b => b.Course).WithMany(b => b.Comments).HasForeignKey(b => b.CourseId);

        }

        public override void Build()
        {
            BuildCourseDbTable(_ModelBuilder.Entity<Course>());
            BuildCourseTagDbTable(_ModelBuilder.Entity<CourseTag>());
            BuildCourseUserCreditDbTable(_ModelBuilder.Entity<CourseUserCredit>());
            BuildCourseCommentDbTable(_ModelBuilder.Entity<CourseComment>());
        }
    }
}
