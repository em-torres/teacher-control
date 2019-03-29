using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherControl.Core.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildCourseModel
    {
        public static ModelBuilder BuildCourse(this ModelBuilder builder)
        {
            EntityTypeBuilder<Course> model = builder.Entity<Course>();

            model.HasKey(b => b.Id);

            model.Property(b => b.Name).IsRequired().HasMaxLength(150);
            model.Property(b => b.HashIndex).IsRequired().HasValueGenerator<TokenGuidGenerator>().HasMaxLength(15);
            model.Property(b => b.Description).HasMaxLength(5000).IsRequired(); //TODO: max length TBD
            model.Property(b => b.Credits).IsRequired();
            model.Property(b => b.CodeId).HasMaxLength(15).IsRequired();
            model.Property(b => b.StartDate).IsRequired();
            model.Property(b => b.EndDate).IsRequired();
            model.Property(b => b.Credits).IsRequired();
            model.Property(b => b.Status).HasMaxLength(15).IsRequired();

            model.HasIndex(b => b.HashIndex).IsUnique();
            model.HasIndex(b => b.CodeId).IsUnique();
            
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

            model.Property(b => b.Credits).HasDefaultValue(0);

            model.HasOne(b => b.Student)
                .WithMany(i => i.Credits)
                .HasForeignKey(i => i.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            model.HasOne(b => b.Course)
                .WithMany(i => i.StudentCredits)
                .HasForeignKey(i => i.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            return builder;
        }
    }
}
