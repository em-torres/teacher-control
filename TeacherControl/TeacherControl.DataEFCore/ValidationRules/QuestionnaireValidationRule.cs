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
            BuildQuestionnaireSectionDbTable(_ModelBuilder.Entity<QuestionnaireSection>());
            BuildQuestionAnswerDbTable(_ModelBuilder.Entity<QuestionAnswer>());
        }

        private void BuildQuestionnaireDbTable(EntityTypeBuilder<Questionnaire> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Title).IsRequired().HasMaxLength(150);
            model.Property(b => b.Body).IsRequired().HasMaxLength(600);

            //relations
            model.Ignore(b => b.Status);
            model.Ignore(b => b.Assignment);
            model.HasOne(b => b.Assignment).WithMany(b => b.Questionnaires).HasForeignKey(b => b.AssignmentId);
        }

        private void BuildQuestionnaireSectionDbTable(EntityTypeBuilder<QuestionnaireSection> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Id).ValueGeneratedOnAdd();
            model.Property(b => b.Page).IsRequired();

            //relations
            model.Ignore(b => b.Questionnaire);
            model.HasOne(b => b.Questionnaire).WithMany(b => b.Sections).HasForeignKey(b => b.QuestionnaireId);
        }

        private void BuildQuestionDbTable(EntityTypeBuilder<Question> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Id).ValueGeneratedOnAdd();

            model.Property(b => b.Title).IsRequired();
            model.Property(b => b.Points).IsRequired();
            model.Property(b => b.IsRequired).IsRequired();

            //relations
            model.Ignore(b => b.QuestionnaireSection);
            model.HasOne(b => b.QuestionnaireSection).WithMany(b => b.Questions).HasForeignKey(b => b.QuestionnaireSectionId);
        }

        private void BuildQuestionAnswerDbTable(EntityTypeBuilder<QuestionAnswer> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Id).ValueGeneratedOnAdd();

            model.Property(b => b.Title).IsRequired();
            model.Property(b => b.IsCorrect).IsRequired();
            model.Property(b => b.MaxLength).IsRequired();

            //relations
            model.Ignore(b => b.Question);
            model.HasOne(b => b.Question).WithMany(b => b.Answers).HasForeignKey(b => b.QuestionId);
        }
    }
}
