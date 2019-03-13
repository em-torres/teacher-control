using Microsoft.EntityFrameworkCore;
using TeacherControl.DataEFCore.ValidationRules;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Seeds;

namespace TeacherControl.DataEFCore
{
    public class TCContext : DbContext
    {
        public virtual DbContextOptions<TCContext> _Options { get; set; }
        //TODO: recheck the dbsets if follows the EF core conventions
        #region Assignment DB Sets
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentStudentPoint> AssignmentStudentPoints { get; set; }
        public virtual DbSet<AssignmentGroup> AssignmentGroups { get; set; }
        public virtual DbSet<AssignmentTag> AssignmentTags { get; set; }
        #endregion

        #region Questionnaire DB Sets
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireCommitment> QuestionnaireCommitments { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionAnswerMatch> QuestionAnswerMatches { get; set; }
        public virtual DbSet<UserAnswer> QuestionAnswerUsers { get; set; }
        public virtual DbSet<UserAnswerMatch> QuestionAnswerUserMatches { get; set; }
        #endregion

        #region Commitment Db Sets
        public virtual DbSet<Commitment> Commitments { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers{ get; set; }
        public virtual DbSet<UserAnswerMatch> UserAnswerMatches { get; set; }
        public virtual DbSet<UserOpenResponseAnswer> UserOpenResponseAnswers { get; set; }
        #endregion

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTag> CourseTags { get; set; }
        public virtual DbSet<CourseComment> CourseComments { get; set; }
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