using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherControl.Core.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildAssignmentModel
    {

        public static ModelBuilder BuildAssignment(this ModelBuilder builder)
        {
            EntityTypeBuilder<Assignment> model = builder.Entity<Assignment>();

            model.Property(b => b.Title).IsRequired().HasMaxLength(150);
            model.Property(b => b.HashIndex).IsRequired().HasValueGenerator<TokenGuidGenerator>().HasMaxLength(15);
            model.Property(b => b.StartDate).IsRequired();
            model.Property(b => b.EndDate).IsRequired();
            model.Property(b => b.Body).IsRequired().HasMaxLength(5000);
            model.Property(b => b.Points).IsRequired();
            model.Property(b => b.Status).IsRequired();

            model.HasIndex(b => b.Title).IsUnique();
            model.HasIndex(b => b.HashIndex).IsUnique();

            model.HasOne(b => b.Course)
                .WithMany(b => b.Assignments)
                .HasForeignKey(b => b.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            return builder;
        }

        public static ModelBuilder BuildAssignmentTag(this ModelBuilder builder)
        {
            EntityTypeBuilder<AssignmentTag> model = builder.Entity<AssignmentTag>();

            model.HasKey(b => b.Id);
            model.Property(b => b.Name).IsRequired().HasMaxLength(30);

            model
               .HasOne(b => b.Assignment)
               .WithMany(b => b.Tags)
               .HasForeignKey(b => b.AssignmentId)
               .OnDelete(DeleteBehavior.Cascade);

            return builder;
        }

        public static ModelBuilder BuildAssignmentComment(this ModelBuilder builder)
        {
            EntityTypeBuilder<AssignmentComment> model = builder.Entity<AssignmentComment>();

            model.Property(b => b.Title).HasMaxLength(150).IsRequired();
            model.Property(b => b.Body).HasMaxLength(500).IsRequired();
            model.Property(b => b.Status).IsRequired();

            model
                .HasOne(b => b.Assignment)
                .WithMany(b => b.Comments)
                .HasForeignKey(b => b.AssignmentId);

            model
                .HasMany(i => i.Upvotes)
                .WithOne(i => i.AssignmentComment)
                .HasForeignKey(i => i.AssignmentCommentId);

            model
                .HasMany(i => i.Downvotes)
                .WithOne(i => i.AssignmentComment)
                .HasForeignKey(i => i.AssignmentCommentId);

            return builder;
        }

        public static ModelBuilder BuildAssignmentNote(this ModelBuilder builder)
        {
            EntityTypeBuilder<AssignmentNote> model = builder.Entity<AssignmentNote>();

            model.Property(i => i.Note).IsRequired();

            return builder;
        }
    }
}
