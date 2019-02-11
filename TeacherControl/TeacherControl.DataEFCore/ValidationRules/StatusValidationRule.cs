using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Models;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class StatusValidationRule : BaseValidationRule
    {
        public StatusValidationRule(ModelBuilder builder) : base(builder)
        {

        }

        private void BuildGroupDbTable(EntityTypeBuilder<Status> model)
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Id).ValueGeneratedOnAdd();

            model.Property(b => b.Name).IsRequired().HasMaxLength(50);
            model.HasIndex(i => i.Name).IsUnique();
        }

        public override void Build()
        {
            BuildGroupDbTable(_ModelBuilder.Entity<Status>());
        }
    }
}
