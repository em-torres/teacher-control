using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherControl.Domain.Models;
using TeacherControl.DataEFCore.Generators;
using TeacherControl.DataEFCore.ValidationRules;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class AssignmentValidationRule : BaseValidationRule
    {

        public AssignmentValidationRule(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        private void BuildAssignmentDbTable(EntityTypeBuilder<Assignment> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Title).IsRequired().HasMaxLength(150);
            model.Property(b => b.HashIndex).IsRequired().HasValueGenerator<TokenGuidGenerator>().HasMaxLength(15);
            model.Property(b => b.StartDate).IsRequired();
            model.Property(b => b.EndDate).IsRequired();
            model.Property(b => b.Body).IsRequired();
            model.Property(b => b.Points).IsRequired();

            model.HasIndex(b => b.Title).IsUnique();
            model.HasIndex(b => b.HashIndex).IsUnique();

            //relations
            //model.Ignore(b => b.Status);
        }

        private void BuildAssignmentTagDbTable(EntityTypeBuilder<AssignmentTag> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Name).IsRequired().HasMaxLength(30);

            model
               .HasOne(b => b.Assignment)
               .WithMany(b => b.Tags)
               .HasForeignKey(b => b.AssignmentId)
               .OnDelete(DeleteBehavior.Cascade);
        }

        public void BuildAssignmentGroupDbTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignmentGroup>().HasKey(t => new { t.GroupId, t.AssignmentId });

            modelBuilder.Entity<AssignmentGroup>().HasOne(i => i.Group).WithMany(i => i.Assignments);
            modelBuilder.Entity<AssignmentGroup>().HasOne(i => i.Assignment).WithMany(i => i.Groups);
        }

        public void BuildAssignmentCarouselDbTable(EntityTypeBuilder<AssignmentCarousel> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Title).IsRequired().HasMaxLength(30);
            model.Property(b => b.Order).IsRequired();
            model.Property(b => b.Url).IsRequired().HasMaxLength(400);
            model.Property(b => b.UrlType).IsRequired();

            model
               .HasOne(b => b.Assignment)
               .WithMany(b => b.Carousels)
               .HasForeignKey(b => b.AssignmentId)
               .OnDelete(DeleteBehavior.Cascade);
        }

        public void BuildAssignmentCommentDbTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignmentComment>().HasKey(t => new { t.AssignmentId, t.CommentId });

            modelBuilder.Entity<AssignmentComment>().HasOne(i => i.Comment).WithMany(i => i.Assignments);
            modelBuilder.Entity<AssignmentComment>().HasOne(i => i.Assignment).WithMany(i => i.Comments);
        }

        public void BuildAssignmentCOuntsDbTable(EntityTypeBuilder<AssignmentCounts> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.UpvotesCount).IsRequired().HasDefaultValue(0);
            model.Property(b => b.ViewsCount).IsRequired().HasDefaultValue(0);

            model.HasOne(b => b.Assignment);
        }

        public override void Build()
        {
            BuildAssignmentDbTable(_ModelBuilder.Entity<Assignment>());
            BuildAssignmentTagDbTable(_ModelBuilder.Entity<AssignmentTag>());
            BuildAssignmentCarouselDbTable(_ModelBuilder.Entity<AssignmentCarousel>());

            BuildAssignmentGroupDbTable(_ModelBuilder);
        }
    }
}
