using Microsoft.EntityFrameworkCore;
using TeacherControl.DataEFCore.ValidationRules;
using TeacherControl.Domain.Models;
//using TeacherControl.Infrastructure.ModelValidationRules;
using TeacherControl.Infrastructure.Seeds;

namespace TeacherControl.DataEFCore
{
    public class TCContext : DbContext
    {

        private readonly DbContextOptions<TCContext> _Options;

        #region DbSets
        public readonly DbSet<Assignment> Assignments;
        public readonly DbSet<AssignmentTag> AssignmentTag;
        public readonly DbSet<AssignmentType> AssignmentType;
        public readonly DbSet<AssignmentComment> AssignmentComment;
        public readonly DbSet<AssignmentGroup> AssignmentGroup;
        public readonly DbSet<AssignmentCarousel> AssignmentCarousel;
        public readonly DbSet<Questionnaire> Questionnaire;
        public readonly DbSet<QuestionnaireSection> QuestionnaireSection;
        public readonly DbSet<Question> Question;
        public readonly DbSet<QuestionAnswer> QuestionAnswer;
        public readonly DbSet<Course> Course;
        public readonly DbSet<CourseTag> CourseTag;
        public readonly DbSet<CourseType> CourseType;
        public readonly DbSet<CourseUserCredit> CourseUserCredit;
        public readonly DbSet<Group> Group;
        public readonly DbSet<Status> Status;
        public readonly DbSet<User> User;
        public readonly DbSet<Comment> Comment;
        #endregion

        public TCContext(DbContextOptions<TCContext> options) : base(options)
        {
            _Options = options;
        }

        public TCContext()
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        optionsBuilder.UseLazyLoadingProxies();
        //        optionsBuilder.UseSqlServer(configuration["ConnectionStrings:TeacherControlDb"]);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Validations(modelBuilder).BuildRules();
            new DbSeeds(modelBuilder);
        }

    }
}