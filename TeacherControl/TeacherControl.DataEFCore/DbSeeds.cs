using Microsoft.EntityFrameworkCore;
using System;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.Seeds
{
    public class DbSeeds
    {
        protected ModelBuilder _ModelBuilder;

        public DbSeeds(ModelBuilder modelBuilder)
        {
            _ModelBuilder = modelBuilder;

            StatusSeeds();
            GroupSeeds();
            UserSeeds();
        }

        #region Status
        public void StatusSeeds()
        {
            _ModelBuilder
                .Entity<Status>(opt =>
                    {
                        opt.HasData(new Status { Id = 1, Name = "Active", },
                                    new Status { Id = 2, Name = "InActive" },
                                    new Status { Id = 3, Name = "Pending" },
                                    new Status { Id = 4, Name = "Deprecated" },
                                    new Status { Id = 5, Name = "Bbocked" });

                    });
        }

        #endregion

        #region Group
        public void GroupSeeds()
        {
            _ModelBuilder
                .Entity<Group>(opt =>
                {
                    opt.HasData(new Group
                    {
                        Id = 1,
                        Name = "Test",
                        CreatedBy = "Test",
                        UpdatedBy = "Test",
                        StatusId = 1,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    });
                });
        }
        #endregion

        #region User
        public void UserSeeds()
        {
            _ModelBuilder
                .Entity<User>().HasData(
                    new User { Id = 1, AuthToken = string.Join("", Guid.NewGuid().ToString().Split('-')), CreatedBy = "Test", UpdatedBy = "Test", CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow });
        }
        #endregion

    }
}
