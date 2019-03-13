using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuilderGroupModel
    {
        public static ModelBuilder BuildGroup(this ModelBuilder builder)
        {
            EntityTypeBuilder<Group> model = builder.Entity<Group>();

            model.Property(b => b.Name).IsRequired().HasMaxLength(50);

            model.HasIndex(i => i.Name).IsUnique();
            model.HasIndex(b => b.StatusId).IsUnique(false);

            model.HasOne(b => b.Status).WithOne().HasForeignKey<Group>(b => b.StatusId).OnDelete(DeleteBehavior.Restrict);

            return builder;
        }


        public static ModelBuilder BuildPrivilege(this ModelBuilder builder)
        {
            EntityTypeBuilder<GroupPrivilege> model = builder.Entity<GroupPrivilege>();

            model.Property(b => b.Name).IsRequired().HasMaxLength(50);

            model
                .HasOne(b => b.Group)
                .WithMany(b => b.Privileges)
                .HasForeignKey(b => b.GroupId);

            return builder;
        }
    }
}
