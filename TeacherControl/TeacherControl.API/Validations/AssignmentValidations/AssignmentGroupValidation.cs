using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class AssignmentGroupValidator : AbstractValidator<string>
    {
        public AssignmentGroupValidator()
        {
            RuleFor(m => m).NotEmpty().MinimumLength(3).MaximumLength(30);
        }
    }
}
