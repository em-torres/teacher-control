using Microsoft.EntityFrameworkCore;
using TeacherControl.DataEFCore.ValidationRules;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Seeds;

namespace TeacherControl.DataEFCore
{
    public class TCContext : DbContext
    {
        public virtual DbContextOptions<TCContext> _Options { get; set; }

        #region DbSets
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentTag> AssignmentTags { get; set; }
        public virtual DbSet<AssignmentComment> AssignmentComments { get; set; }
        public virtual DbSet<AssignmentGroup> AssignmentGroups { get; set; }
        public virtual DbSet<AssignmentCarousel> AssignmentCarousels { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireSection> QuestionnaireSections { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTag> CourseTags { get; set; }
        public virtual DbSet<CourseType> CourseTypes { get; set; }
        public virtual DbSet<CourseUserCredit> CourseUserCredits { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        #endregion

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