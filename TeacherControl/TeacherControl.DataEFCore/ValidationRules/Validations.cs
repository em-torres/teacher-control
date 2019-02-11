using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.DataEFCore.ValidationRules
{
    public class Validations
    {
        private readonly AssignmentValidationRule assignmentValidation;
        private readonly CourseValidationRule courseValidationRules;
        private readonly GroupValidationRule groupValidationRules;
        private readonly StatusValidationRule statusValidationRules;
        private readonly UserBuilderValidator userBuilderValidator;
        private readonly QuestionnaireValidationRule questionnaireValidationRules;
        private readonly CommentValidationRule commentValidationRules;

        public Validations(ModelBuilder builder)
        {
            assignmentValidation = new AssignmentValidationRule(builder);
            courseValidationRules = new CourseValidationRule(builder);
            groupValidationRules = new GroupValidationRule(builder);
            statusValidationRules = new StatusValidationRule(builder);
            userBuilderValidator = new UserBuilderValidator(builder);
            questionnaireValidationRules = new QuestionnaireValidationRule(builder);
            commentValidationRules = new CommentValidationRule(builder);
        }

        public void BuildRules()
        {
            assignmentValidation.Build();
            courseValidationRules.Build();
            statusValidationRules.Build();
            groupValidationRules.Build();
            userBuilderValidator.Build();
            questionnaireValidationRules.Build();
            commentValidationRules.Build();
        }
    }
}
