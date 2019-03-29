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
                .BuildAssignmentNote()
                .BuildAssignmentComment();

            modelBuilder
                .BuildCourse()
                .BuildCourseTag()
                .BuildCourseUserCredit();

            modelBuilder
                .BuildUser()
                .BuildUserInfo();

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
