using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using TeacherControl.Domain.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public abstract class BaseValidationRule
    {
        protected readonly ModelBuilder _ModelBuilder;

        public BaseValidationRule(ModelBuilder modelBuilder)
        {
            _ModelBuilder = modelBuilder;
        }

        public abstract void Build();

        protected void BuildBaseModelDbTable<T>(EntityTypeBuilder<T> model) where T : BaseModel
        {
            model.HasKey(b => b.Id);
            model.Property(b => b.Id).ValueGeneratedOnAdd();

            model.Property(b => b.CreatedBy).IsRequired().HasMaxLength(50);
            model.Property(b => b.UpdatedBy).IsRequired().HasMaxLength(50).HasDefaultValue(string.Empty);
            model.Property(b => b.CreatedDate).IsRequired().HasValueGenerator<OnCreateOrUpdateDatetimeGenerator>().ValueGeneratedOnAdd();
            model.Property(b => b.UpdatedDate).IsRequired().HasValueGenerator<OnCreateOrUpdateDatetimeGenerator>().ValueGeneratedOnUpdate();
        }
    }
}
