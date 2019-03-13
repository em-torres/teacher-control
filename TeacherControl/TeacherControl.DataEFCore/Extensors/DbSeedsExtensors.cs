using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Helpers;
using TeacherControl.Core.Models;
using TeacherControl.MockData;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class DbSeedsExtensors
    {
        public static ModelBuilder ApplyDbSeeds(this ModelBuilder builder)
        {
            //TODO: Logger here to write, migration log and which environment is being running
            if (EnvironmentHelper.IsDevelopment()) ApplyDummyDbSeeds(builder);
            if (EnvironmentHelper.IsProduction()) ApplyProdDbSeeds(builder);

            return builder;
        }

        private static void ApplyProdDbSeeds(ModelBuilder builder)
        {

        }

        private static void ApplyDummyDbSeeds(ModelBuilder builder)
        {
            IEnumerable<Status> statuses = StatusFactoryData.CreateUserList();
            //IEnumerable<Group> groups = GroupFactoryData.CreateGroupList(25);
            IEnumerable<User> users = UserFactoryData.CreateUserList(100);

            IEnumerable<Course> courses = CourseFactoryData.CreateCourseList(50).Select((e, i) => { e.StatusId = (i % 7) + 1; return e; });
            IEnumerable<CourseTag> courseTags = CourseFactoryData.CreateCourseTagList(50).Select((e, i) => { e.CourseId = i % 50 + 1; return e; });

            IEnumerable<Assignment> assignments = AssignmentFactoryData.CreateAssignmentList(120).Select((e, i) => { e.CourseId = i % 50 + 1; e.StatusId = (i % 7) + 1; return e; });
            //IEnumerable<AssignmentCounts> assignmentCounts = AssignmentFactoryData.CreateAssignmentCountList(120).Select((e, i) => { e.AssignmentId = i + 1; return e; });

            builder
                .Entity<Status>(opt => opt.HasData(statuses))
                //.Entity<Group>(opt => opt.HasData(groups))
                .Entity<Course>(opt => opt.HasData(courses))
                .Entity<Assignment>(opt => opt.HasData(assignments))
                //.Entity<AssignmentCounts>(opt => opt.HasData(assignmentCounts))
                .Entity<User>(opt => opt.HasData(users));
        }
    }
}
