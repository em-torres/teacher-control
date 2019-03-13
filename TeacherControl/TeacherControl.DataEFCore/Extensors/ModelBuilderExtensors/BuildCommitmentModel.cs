using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildCommitmentModel
    {
        public static ModelBuilder BuildCommitment(this ModelBuilder builder)
        {
            EntityTypeBuilder<Commitment> model = builder.Entity<Commitment>();

            model.HasOne(b => b.User);
            model.HasMany(b => b.Answers).WithOne(b => b.Commitment).HasForeignKey(b => b.CommitmentId);

            model.HasOne(b => b.User);
            model.HasMany(b => b.Matches).WithOne(b => b.Commitment).HasForeignKey(b => b.CommitmentId);

            return builder;
        }

        public static ModelBuilder BuildUserAnswer(this ModelBuilder builder)
        {
            EntityTypeBuilder<UserAnswer> model = builder.Entity<UserAnswer>();

            model.HasKey(i => i.Id);

            model.HasOne(b => b.QuestionAnswer);
            model.HasOne(b => b.Commitment).WithMany(b => b.Answers).HasForeignKey(b => b.CommitmentId);

            return builder;
        }

        public static ModelBuilder BuildUserAnswerMatch(this ModelBuilder builder)
        {
            EntityTypeBuilder<UserAnswerMatch> model = builder.Entity<UserAnswerMatch>();

            model.HasKey(i => i.Id);

            model.Property(i => i.LeftQuestionAnswerId).IsRequired();
            model.Property(i => i.RightQuestionAnswerId).IsRequired();

            model.HasOne(q => q.QuestionAnswer);
            model.HasOne(q => q.Commitment).WithMany(b => b.Matches).HasForeignKey(b => b.CommitmentId);

            return builder;
        }

        public static ModelBuilder BuildUserOpenResponseAnswer(this ModelBuilder builder)
        {
            EntityTypeBuilder<UserOpenResponseAnswer> model = builder.Entity<UserOpenResponseAnswer>();

            model.HasKey(i => i.Id);

            model.Property(i => i.UserResponse).IsRequired();

            model.HasOne(q => q.Question);
            model.HasOne(q => q.Commitment).WithMany(b => b.OpenResponses).HasForeignKey(b => b.CommitmentId);

            return builder;
        }
    }
}
