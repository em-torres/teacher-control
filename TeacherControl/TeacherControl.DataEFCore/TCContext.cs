﻿using Microsoft.EntityFrameworkCore;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using TeacherControl.Domain.Services;

namespace TeacherControl.DataEFCore
{
    public class TCContext : DbContext
    {
        protected DbContextOptions<TCContext> _Options { get; set; }
        protected IUserService _UserService;

        //TODO: re-check the dbsets if follows the EF core conventions
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
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<UserAnswerMatch> UserAnswerMatches { get; set; }
        public virtual DbSet<UserOpenResponseAnswer> UserOpenResponseAnswers { get; set; }
        #endregion

        #region Courses
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTag> CourseTags { get; set; }
        public virtual DbSet<CourseComment> CourseComments { get; set; }
        public virtual DbSet<CourseUserCredit> CourseUserCredits { get; set; }
        #endregion

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public TCContext(DbContextOptions<TCContext> options, IUserService userService) : base(options)
        {
            _Options = options;
            _UserService = userService;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .BuildModelValidationRules()
                .ApplyDbSeeds();
        }

        public override int SaveChanges()
        {
            if (ChangeTracker.HasChanges())
            {
                ChangeTracker
                    .ApplyAuditInformation(_UserService);

                return base.SaveChanges();
            }
            //return no-change enum value

            return 0;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (ChangeTracker.HasChanges())
            {
                ChangeTracker
                    .ApplyAuditInformation(_UserService);

                return base.SaveChangesAsync();
            }

            //return no-change enum value
            return Task.FromResult(0);
        }
    }
}