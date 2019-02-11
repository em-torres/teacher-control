using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.DataEFCore.ValidationRules;
using TeacherControl.Domain.Models;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class GroupValidationRule : BaseValidationRule
    {
        public GroupValidationRule(ModelBuilder builder) : base(builder) 
        {

        }

        private void BuildGroupDbTable(EntityTypeBuilder<Group> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Name).IsRequired().HasMaxLength(50);

            model.HasIndex(i => i.Name).IsUnique();
            model.Ignore(b => b.Status);
        }


        private void BuildPrivilegeDbtable(EntityTypeBuilder<GroupPrivilege> model)
        {
            BuildBaseModelDbTable(model);

            model.Property(b => b.Name).IsRequired().HasMaxLength(50);

            model
                .HasOne(b => b.Group)
                .WithMany(b => b.Privileges)
                .HasForeignKey(b => b.GroupId);
        }
        public override void Build()
        {
            BuildGroupDbTable(_ModelBuilder.Entity<Group>());
            BuildPrivilegeDbtable(_ModelBuilder.Entity<GroupPrivilege>());
        }
    }
}
