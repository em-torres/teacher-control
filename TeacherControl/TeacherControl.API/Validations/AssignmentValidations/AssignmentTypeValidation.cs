using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class AssignmentTypeValidation : AbstractValidator<string>
    {
        public AssignmentTypeValidation()
        {
            RuleFor(m => m).NotEmpty().MinimumLength(3).MaximumLength(30);
        }
    }
}
