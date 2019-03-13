using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildStatusModel
    {
        public static ModelBuilder BuildStatus(this ModelBuilder builder)
        {
            EntityTypeBuilder<Status> model = builder.Entity<Status>();

            model.HasKey(b => b.Id);
            model.Property(b => b.Id).ValueGeneratedOnAdd();

            model.Property(b => b.Name).IsRequired().HasMaxLength(50);
            model.HasIndex(i => i.Name).IsUnique();

            return builder;
        }
    }
}
