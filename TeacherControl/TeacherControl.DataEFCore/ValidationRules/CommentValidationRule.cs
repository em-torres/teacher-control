using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.DataEFCore.ValidationRules;
using TeacherControl.Domain.Models;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class CommentValidationRule : BaseValidationRule
    {
        public CommentValidationRule(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        //private void BuildCommentDbTable(EntityTypeBuilder<Commentary> model)
        //{
        //    BuildBaseModelDbTable(model);

        //    model.Property(b => b.Title).IsRequired().HasMaxLength(150);
        //    model.Property(b => b.Body).IsRequired().HasMaxLength(500);
        //    model.Property(b => b.Upvote).IsRequired();

        //    //relations
        //    model.HasOne(i => i.Author);
        //}

        public override void Build()
        {
            //BuildCommentDbTable(_ModelBuilder.Entity<Commentary>());
        }
    }
}
