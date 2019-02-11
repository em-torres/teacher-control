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

            model.Property(b => b.Name).IsRequired().HasMaxLength(60);
            model.Property(b => b.Description).IsRequired().HasMaxLength(300);
            model.Property(b => b.Credits).IsRequired().IsRequired();
            model.Property(b => b.StartDate).IsRequired();
            model.Property(b => b.Credits).IsRequired();

            //relations
            model.HasMany(b => b.Tags);
            model.HasMany(b => b.Types);
            model.HasOne(b => b.Professor);
        }

        private void BuildCourseTypeDbTable(EntityTypeBuilder<CourseType> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Name).IsRequired().HasMaxLength(60);
            model.Property(b => b.Description).IsRequired().HasMaxLength(300);
        }

        private void BuildCourseTagDbTable(EntityTypeBuilder<CourseTag> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Name).IsRequired().HasMaxLength(30);
        }

        private void BuildCourseUserCreditDbTable(EntityTypeBuilder<CourseUserCredit> model)
        {
            BuildBaseModelDbTable(model);
            model.Property(b => b.Credits).IsRequired();

            model.HasOne(b => b.Student);
            model.HasOne(b => b.Course);
        }

        public override void Build()
        {
            BuildCourseDbTable(_ModelBuilder.Entity<Course>());
            BuildCourseTagDbTable(_ModelBuilder.Entity<CourseTag>());
            BuildCourseTypeDbTable(_ModelBuilder.Entity<CourseType>());
            BuildCourseUserCreditDbTable(_ModelBuilder.Entity<CourseUserCredit>());
        }
    }
}
