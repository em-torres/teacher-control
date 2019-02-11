using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class UserBuilderValidator : BaseValidationRule
    {
        public UserBuilderValidator(ModelBuilder builder) : base(builder)
        {

        }

        public override void Build()
        {
            BuildUserGroupDbTable(_ModelBuilder);
            BuildUserDbtable(_ModelBuilder.Entity<User>());
        }

        private void BuildUserDbtable(EntityTypeBuilder<User> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(i => i.AuthToken).IsRequired().HasMaxLength(50).HasValueGenerator(typeof(TokenGuidGenerator)).ValueGeneratedOnAdd();
            model.HasOne(i => i.UserInfo);

            model.HasIndex(i => i.AuthToken).IsUnique();

        }

        private void BuildUserGroupDbTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasKey(t => new { t.GroupId, t.UserId });

            modelBuilder.Entity<UserGroup>().HasOne(i => i.Group).WithMany(i => i.Users).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserGroup>().HasOne(i => i.User).WithMany(i => i.Groups).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
