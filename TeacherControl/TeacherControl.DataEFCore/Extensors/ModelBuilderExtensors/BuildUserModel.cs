using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherControl.Core.Enums;
using TeacherControl.Core.Models;
using TeacherControl.DataEFCore.Generators;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class BuildUserModel
    {
        public static ModelBuilder BuildUser(this ModelBuilder builder)
        {
            EntityTypeBuilder<User> model = builder.Entity<User>();

            model.Property(i => i.Password).IsRequired().HasMaxLength(100);
            model.Property(i => i.SaltToken).IsRequired().HasMaxLength(32).HasValueGenerator(typeof(TokenGuidGenerator)).ValueGeneratedOnAdd();
            model.Property(i => i.Status).IsRequired();
            model.Property(i => i.Email).IsRequired().HasMaxLength(100);
            model.Property(i => i.Username).IsRequired().HasMaxLength(100);
            model.Property(i => i.Status).HasDefaultValue((int) Status.Active);

            model.HasIndex(i => i.SaltToken).IsUnique();
            model.HasIndex(i => i.Email).IsUnique();
            model.HasIndex(i => i.Username).IsUnique();

            return builder;
        }

        public static ModelBuilder BuildUserInfo(this ModelBuilder builder)
        {
            EntityTypeBuilder<UserInfo> model = builder.Entity<UserInfo>();

            model.Property(i => i.FirstName).IsRequired().HasMaxLength(100);
            model.Property(i => i.LastName).IsRequired().HasMaxLength(100);
            model.Property(i => i.PhoneNumber).IsRequired().HasMaxLength(20);
            model.Property(i => i.Gender).IsRequired().HasMaxLength(20);
            model.Property(i => i.Birthday).IsRequired();
            model.Property(i => i.CodeId).IsRequired().HasMaxLength(20);

            model.HasIndex(i => i.CodeId).IsUnique();
            model.HasIndex(i => i.PhoneNumber).IsUnique();

            model.HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<UserInfo>(i => i.UserId);

            return builder;
        }
    }
}
