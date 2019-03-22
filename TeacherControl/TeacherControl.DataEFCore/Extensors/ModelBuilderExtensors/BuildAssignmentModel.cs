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

            model.HasIndex(b => b.Title).IsUnique();
            model.HasIndex(b => b.HashIndex).IsUnique();
            model.HasIndex(b => b.StatusId).IsUnique(false);

            model.HasOne(b => b.Course)
                .WithMany(b => b.Assignments)
                .HasForeignKey(b => b.CourseId);

            model.HasOne(b => b.Status)
                .WithOne()
                .HasForeignKey<Assignment>(i => i.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            model
                .HasOne(b => b.Counts)
                .WithOne(i => i.Assignment)
                .HasForeignKey<AssignmentCounts>(i => i.AssignmentId);

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

        public static ModelBuilder BuildAssignmentCounts(this ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<AssignmentCounts> model = modelBuilder.Entity<AssignmentCounts>();

            model.HasKey(b => b.Id);
            model.Property(b => b.UpvotesCount).IsRequired().HasDefaultValue(0);
            model.Property(b => b.ViewsCount).IsRequired().HasDefaultValue(0);

            return modelBuilder;
        }
        public static ModelBuilder BuildAssignmentComment(this ModelBuilder builder)
        {
            EntityTypeBuilder<AssignmentComment> model = builder.Entity<AssignmentComment>();

            model.Property(b => b.Title).IsRequired();
            model.Property(b => b.Upvote).HasDefaultValue(0).IsRequired();
            model.Property(b => b.Body).IsRequired();

            model.HasIndex(b => b.StatusId).IsUnique(false);
            model.HasIndex(b => b.AuthorId).IsUnique(false);

            model.HasOne(b => b.Author)
                .WithOne()
                .HasForeignKey<AssignmentComment>(i => i.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            model.HasOne(b => b.Status)
                .WithOne()
                .HasForeignKey<AssignmentComment>(b => b.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            model
                .HasOne(b => b.Assignment)
                .WithMany(b => b.Comments)
                .HasForeignKey(b => b.AssignmentId);

            return builder;
        }

    }
}
