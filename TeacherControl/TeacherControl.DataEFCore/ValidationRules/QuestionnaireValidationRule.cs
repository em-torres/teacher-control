using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class QuestionnaireValidationRule : BaseValidationRule
    {
        public QuestionnaireValidationRule(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public override void Build()
        {
            BuildQuestionDbTable(_ModelBuilder.Entity<Question>());
            BuildQuestionnaireDbTable(_ModelBuilder.Entity<Questionnaire>());
            BuildQuestionAnswerDbTable(_ModelBuilder.Entity<QuestionAnswer>());
            BuildQuestionAnswerMatchDbTable(_ModelBuilder.Entity<QuestionAnswerMatch>());

        }

        private void BuildQuestionnaireDbTable(EntityTypeBuilder<Questionnaire> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Title).IsRequired().HasMaxLength(150);
            model.Property(b => b.Body).IsRequired().HasMaxLength(600);

            //relations
            model.HasMany(b => b.Questions);
            model.HasOne(b => b.Assignment)
                .WithMany(b => b.Questionnaires)
                .HasForeignKey(b => b.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
        private void BuildQuestionDbTable(EntityTypeBuilder<Question> model)
        {
            model.HasKey(b => b.Id);

            model.Property(b => b.HeadLine).IsRequired();
            model.Property(b => b.Points).IsRequired();
            model.Property(b => b.IsRequired).IsRequired();
        }

        private void BuildQuestionAnswerDbTable(EntityTypeBuilder<QuestionAnswer> model)
        {
            model.HasKey(b => b.Id);

            model.Property(b => b.Answer).IsRequired();
            model.Property(b => b.IsCorrect).IsRequired();

            model.HasOne(b => b.Question).WithMany(b => b.Answers).HasForeignKey(b => b.QuestionId);
        }

        private void BuildQuestionAnswerMatchDbTable(EntityTypeBuilder<QuestionAnswerMatch> model)
        {
            model.HasKey(b => b.Id);

            model.Property(b => b.LeftQuestionAnswerId).IsRequired();
            model.Property(b => b.RightQuestionAnswerId).IsRequired();

            model
                .HasOne(b => b.Question)
                .WithMany(b => b.AnswerMatches)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
