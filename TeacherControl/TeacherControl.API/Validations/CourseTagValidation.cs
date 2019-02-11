using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class CourseTagValidation : AbstractValidator<string>
    {
        public CourseTagValidation()
        {
            RuleFor(m => m).NotEmpty().MinimumLength(5).MaximumLength(30);
        }
    }
}
