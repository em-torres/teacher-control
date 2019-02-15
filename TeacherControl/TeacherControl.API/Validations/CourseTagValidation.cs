using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class CourseTagValidation : AbstractValidator<string>
    {
        public CourseTagValidation()
        {
            RuleFor(m => m).NotEmpty().Matches(@"[\w\-@#]{3,30}");
        }
    }
}
