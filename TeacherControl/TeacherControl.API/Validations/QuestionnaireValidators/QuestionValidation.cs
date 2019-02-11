using FluentValidation;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class QuestionValidation : AbstractValidator<QuestionDTO>
    {
        public QuestionValidation()
        {
            RuleFor(b => b.IsRequired).NotNull().NotEmpty();
            RuleFor(b => b.Points).GreaterThan(0);
            RuleFor(b => b.Title).NotEmpty();

            RuleForEach(b => b.Answers).SetValidator(new QuestionAnswerValidation());
        }
    }
}
