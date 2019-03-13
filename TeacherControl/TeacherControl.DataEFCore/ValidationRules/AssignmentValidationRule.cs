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
            model.Property(b => b.Body).IsRequired().HasMaxLength(5000);
            model.Property(b => b.Points).IsRequired();

            model.HasIndex(b => b.Title).IsUnique();
            model.HasIndex(b => b.HashIndex).IsUnique();
            model.HasOne(b => b.Course)
                .WithMany(b => b.Assignments)
                .HasForeignKey(b => b.CourseId);

            model.HasOne(b => b.Status)
                .WithOne()
                .HasForeignKey<Assignment>(b => b.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
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

        private void BuildAssignmentGroupDbTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignmentGroup>().HasKey(t => new { t.GroupId, t.AssignmentId });

            modelBuilder.Entity<AssignmentGroup>().HasOne(i => i.Group).WithMany(i => i.Assignments);
            modelBuilder.Entity<AssignmentGroup>().HasOne(i => i.Assignment).WithMany(i => i.Groups);
        }

        public void BuildAssignmentCountsDbTable(EntityTypeBuilder<AssignmentCounts> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.UpvotesCount).IsRequired().HasDefaultValue(0);
            model.Property(b => b.ViewsCount).IsRequired().HasDefaultValue(0);

            model.HasOne(b => b.Assignment);
        }

        private void BuildAssignmentCommitmentDbTable(EntityTypeBuilder<QuestionnaireCommitment> model)
        {
            model.HasKey(b => new { b.QuestionnaireId, b.CommitmentId });

            model.HasOne(b => b.Questionnaire).WithMany(b => b.QuestionnaireCommitments).HasForeignKey(b => b.QuestionnaireId);
            model.HasOne(b => b.Commitment).WithMany(b => b.AssignmentCommitments).HasForeignKey(b => b.CommitmentId);
        }

        public override void Build()
        {
            BuildAssignmentDbTable(_ModelBuilder.Entity<Assignment>());
            BuildAssignmentTagDbTable(_ModelBuilder.Entity<AssignmentTag>());
            BuildAssignmentCountsDbTable(_ModelBuilder.Entity<AssignmentCounts>());
            BuildAssignmentCommitmentDbTable(_ModelBuilder.Entity<QuestionnaireCommitment>());

            BuildAssignmentGroupDbTable(_ModelBuilder);
        }
    }
}
