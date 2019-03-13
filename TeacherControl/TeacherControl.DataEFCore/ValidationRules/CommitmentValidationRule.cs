using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Models;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class CommitmentValidationRule : BaseValidationRule
    {

        public CommitmentValidationRule(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        public override void Build()
        {
            BuildCommitmentDbTable(_ModelBuilder.Entity<Commitment>());
            BuildUserAnswerDbTable(_ModelBuilder.Entity<UserAnswer>());
            BuildUserAnswerMatchDbTable(_ModelBuilder.Entity<UserAnswerMatch>());
            BuildUserOpenResponseAnswerDbTable(_ModelBuilder.Entity<UserOpenResponseAnswer>());
        }

        private void BuildCommitmentDbTable(EntityTypeBuilder<Commitment> model)
        {
            BuildBaseModelDbTable(model);

            model.HasOne(b => b.User);
            model.HasMany(b => b.Answers).WithOne(b => b.Commitment).HasForeignKey(b => b.CommitmentId);

            model.HasOne(b => b.User);
            model.HasMany(b => b.Matches).WithOne(b => b.Commitment).HasForeignKey(b => b.CommitmentId);
        }

        private void BuildUserAnswerDbTable(EntityTypeBuilder<UserAnswer> model)
        {
            model.HasKey(i => i.Id);

            model.HasOne(b => b.QuestionAnswer);
            model.HasOne(b => b.Commitment).WithMany(b => b.Answers).HasForeignKey(b => b.CommitmentId);
        }

        private void BuildUserAnswerMatchDbTable(EntityTypeBuilder<UserAnswerMatch> model)
        {
            model.HasKey(i => i.Id);

            model.Property(i => i.LeftQuestionAnswerId).IsRequired();
            model.Property(i => i.RightQuestionAnswerId).IsRequired();

            model.HasOne(q => q.QuestionAnswer);
            model.HasOne(q => q.Commitment).WithMany(b => b.Matches).HasForeignKey(b => b.CommitmentId);
        }

        private void BuildUserOpenResponseAnswerDbTable(EntityTypeBuilder<UserOpenResponseAnswer> model)
        {
            model.HasKey(i => i.Id);

            model.Property(i => i.UserResponse).IsRequired();

            model.HasOne(q => q.Question);
            model.HasOne(q => q.Commitment).WithMany(b => b.OpenResponses).HasForeignKey(b => b.CommitmentId);
        }
    }
}
