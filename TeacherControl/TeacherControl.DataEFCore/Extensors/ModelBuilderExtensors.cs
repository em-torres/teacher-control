using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class ModelBuilderExtensors
    {

        public static ModelBuilder BuildModelValidationRules(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .BuildAssignment()
                .BuildAssignmentTag()
                .BuildAssignmentComment()
                .BuildAssignmentCounts();

            modelBuilder
                .BuildCourse()
                .BuildCourseTag()
                .BuildCourseUserCredit();

            modelBuilder.BuildStatus();

            modelBuilder
                .BuildGroup();

            modelBuilder
                .BuildUser()
                .BuildUserInfo()
                .BuildUserCourse()
                .BuildUserGroup();

            modelBuilder
                .BuildQuestionnaire()
                .BuildQuestion()
                .BuildQuestionAnswer()
                .BuildQuestionAnswerMatch()
                .BuildQuestionType()
                .BuildQuestionnaireCommitment();

            modelBuilder
                .BuildCommitment()
                .BuildUserAnswer()
                .BuildUserAnswerMatch()
                .BuildUserOpenResponseAnswer();

            return modelBuilder;
        }
    }
}
