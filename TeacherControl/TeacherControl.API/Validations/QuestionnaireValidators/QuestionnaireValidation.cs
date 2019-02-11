using FluentValidation;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class QuestionnaireValidation : AbstractValidator<QuestionnaireDTO>
    {
        public QuestionnaireValidation()
        {
            RuleFor(b => b.Title).MaximumLength(150).NotEmpty();
            RuleFor(b => b.Body).MaximumLength(600).NotEmpty();
            RuleFor(b => b.Status).GreaterThan(0);

        }
    }
}
