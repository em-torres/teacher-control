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
            IEnumerable<User> users = UserFactoryData.CreateUserList(100).Select((e, i) => { e.Status = Core.Enums.Status.Active.ToString(); return e; });
            IEnumerable<UserInfo> userInfos = UserFactoryData.CreateUserInfoList(100).Select((e, i) => { e.UserId = i + 1; return e; });

            IEnumerable<Course> courses = CourseFactoryData.CreateCourseList(50).Select((e, i) => { e.Status = Core.Enums.Status.Active.ToString(); e.ProfessorId = i + 1; return e; });
            IEnumerable<CourseTag> courseTags = CourseFactoryData.CreateCourseTagList(50).Select((e, i) => { e.CourseId = i % 50 + 1; return e; });

            IEnumerable<Assignment> assignments = AssignmentFactoryData.CreateAssignmentList(120).Select((e, i) => { e.CourseId = i % 50 + 1; e.Status = Core.Enums.Status.Active.ToString(); return e; });

            IEnumerable<AssignmentComment> assignmentComments = AssignmentFactoryData
                .CreateAssignmentCommentList(250)
                .Select((e, i) => { e.AssignmentId = i % 50 + 1; e.AuthorId = i % 100 + 1; e.Status = Core.Enums.Status.Active.ToString(); return e; });
            IEnumerable<AssignmentCommentUpvote> assignmentCommentUpvs = AssignmentFactoryData.CreateAssignmentCommentUpvoteList(100).Select((e, i) => { e.AssignmentCommentId = i + 1; e.UserId = i + 1; return e; });
            IEnumerable<AssignmentCommentDownvote> assignmentCommentDownvs = AssignmentFactoryData.CreateAssignmentCommentDownvoteList(100).Select((e, i) => { e.AssignmentCommentId = i + 1; e.UserId = i + 1; return e; });

            builder
                //.Entity<Group>(opt => opt.HasData(groups))
                .Entity<User>(opt => opt.HasData(users))
                .Entity<UserInfo>(opt => opt.HasData(userInfos))
                .Entity<Course>(opt => opt.HasData(courses))
                .Entity<Assignment>(opt => opt.HasData(assignments))
                .Entity<AssignmentComment>(opt => opt.HasData(assignmentComments))
                .Entity<AssignmentCommentUpvote>(opt => opt.HasData(assignmentCommentUpvs))
                .Entity<AssignmentCommentDownvote>(opt => opt.HasData(assignmentCommentDownvs));
        }
    }
}
