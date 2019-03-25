using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildQuestionnaireModel
    {
        public static ModelBuilder BuildQuestionnaire(this ModelBuilder builder)
        {
            EntityTypeBuilder<Questionnaire> model = builder.Entity<Questionnaire>();

            model.Property(b => b.Title).IsRequired().HasMaxLength(150);
            model.Property(b => b.Body).IsRequired().HasMaxLength(600);

            model.HasMany(b => b.Questions);
            model.HasOne(b => b.Assignment)
                .WithMany(b => b.Questionnaires)
                .HasForeignKey(b => b.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            return builder;
        }

        public static ModelBuilder BuildQuestion(this ModelBuilder builder)
        {
            EntityTypeBuilder<Question> model = builder.Entity<Question>();

            model.HasKey(b => b.Id);

            model.Property(b => b.HeadLine).IsRequired();
            model.Property(b => b.Points).IsRequired();
            model.Property(b => b.IsRequired).IsRequired();

            model.HasOne(b => b.QuestionType).WithOne().HasForeignKey<Question>(b => b.QuestionTypeId);

            return builder;
        }

        public static ModelBuilder BuildQuestionAnswer(this ModelBuilder builder)
        {
            EntityTypeBuilder<QuestionAnswer> model = builder.Entity<QuestionAnswer>();

            model.HasKey(b => b.Id);

            model.Property(b => b.Answer).IsRequired();
            model.Property(b => b.IsCorrect).IsRequired();

            model.HasOne(b => b.Question)
                .WithMany(b => b.Answers)
                .HasForeignKey(b => b.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            return builder;
        }

        public static ModelBuilder BuildQuestionAnswerMatch(this ModelBuilder builder)
        {
            EntityTypeBuilder<QuestionAnswerMatch> model = builder.Entity<QuestionAnswerMatch>();

            model.HasKey(b => b.Id);

            model.Property(b => b.LeftQuestionAnswerId).IsRequired();
            model.Property(b => b.RightQuestionAnswerId).IsRequired();

            model
                .HasOne(b => b.Question)
                .WithMany(b => b.AnswerMatches)
                .OnDelete(DeleteBehavior.Restrict);

            return builder;
        }

        public static ModelBuilder BuildQuestionType(this ModelBuilder builder)
        {
            EntityTypeBuilder<QuestionType> model = builder.Entity<QuestionType>();

            model.HasKey(b => b.Id);

            model.Property(b => b.Name).IsRequired();

            return builder;
        }

        public static ModelBuilder BuildQuestionnaireCommitment(this ModelBuilder builder)
        {
            EntityTypeBuilder<QuestionnaireCommitment> model = builder.Entity<QuestionnaireCommitment>();

            model.HasKey(b => new { b.QuestionnaireId, b.CommitmentId });

            model.HasOne(b => b.Questionnaire)
                .WithMany(b => b.QuestionnaireCommitments)
                .HasForeignKey(b => b.QuestionnaireId)
                .OnDelete(DeleteBehavior.Restrict);

            model.HasOne(b => b.Commitment)
                .WithMany(b => b.AssignmentCommitments)
                .HasForeignKey(b => b.CommitmentId)
                .OnDelete(DeleteBehavior.Restrict);

            return builder;
        }
    }
}
