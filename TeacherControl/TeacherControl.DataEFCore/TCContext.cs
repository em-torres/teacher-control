using Microsoft.EntityFrameworkCore;
using TeacherControl.DataEFCore.ValidationRules;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Seeds;

namespace TeacherControl.DataEFCore
{
    public class TCContext : DbContext
    {
        public virtual DbContextOptions<TCContext> _Options { get; set; }

        #region Assignment DB Sets
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentStudentPoint> AssignmentStudentPoints { get; set; }
        public virtual DbSet<AssignmentGroup> AssignmentGroups { get; set; }
        public virtual DbSet<AssignmentTag> AssignmentTags { get; set; }
        #endregion

        #region Questionnaire DB Sets
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionAnswerMatch> QuestionAnswerMatches { get; set; }
        public virtual DbSet<QuestionAnswerUser> QuestionAnswerUsers { get; set; }
        public virtual DbSet<QuestionAnswerUserMatch> QuestionAnswerUserMatches { get; set; }
        #endregion

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTag> CourseTags { get; set; }
        public virtual DbSet<CourseUserCredit> CourseUserCredits { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Commentary> Comments { get; set; }

        public TCContext(DbContextOptions<TCContext> options) : base(options)
        {
            _Options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Validations(modelBuilder).BuildRules();
            new DbSeeds(modelBuilder);
        }

    }
}