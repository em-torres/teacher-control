using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class CourseTypeValidation : AbstractValidator<string>
    {
        public CourseTypeValidation()
        {
            RuleFor(m => m).NotEmpty().MinimumLength(5).MaximumLength(60);

        }
    }
}
