using FluentValidation;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class QuestionAnswerValidation : AbstractValidator<QuestionAnswerDTO>
    {
        public QuestionAnswerValidation()
        {
            RuleFor(b => b.IsCorrect).NotNull().NotEmpty();
            RuleFor(b => b.MaxLength).GreaterThan(0);
            RuleFor(b => b.Title).NotEmpty();
        }

    }
}
