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
            CourseSeed();
            AssignmentSeeds();
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

        #region Course

        public void CourseSeed()
        {
            _ModelBuilder
               .Entity<Course>().HasData(new Course
               {
                   Credits = 123.45,
                   Id = 1,
                   EndDate = DateTime.UtcNow.AddDays(2),
                   StartDate = DateTime.UtcNow,
                   Description = "Lorem Ipsum",
                   Name = "Lorem Ipsum",
                   StatusId = 1,
                   UpdatedBy = "Test Seed",
                   CreatedDate = DateTime.UtcNow,
                   ProfessorId = 1,
                   CreatedBy = "test username",
                   HashIndex = Guid.NewGuid().ToString()
               });
        }

        #endregion

        #region Assignment
        public void AssignmentSeeds()
        {
            Assignment assignment = new Assignment
            {
                Id = 1,
                Body = "Lorem Ipsum",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(2),
                HashIndex = string.Join("", Guid.NewGuid().ToString().Split('-')).Substring(0, 12),
                Points = 1234,
                CourseId = 1,
                Title = "lorem ipsum",
                CreatedBy = "Test",
                UpdatedBy = "Test",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            AssignmentCounts assignmentCounts = new AssignmentCounts { Id = 1, UpvotesCount = 123, ViewsCount = 321, AssignmentId = 1};

            _ModelBuilder.Entity<Assignment>().HasData(assignment);
            _ModelBuilder.Entity<AssignmentCounts>().HasData(assignmentCounts);
        }


        #endregion

    }
}
