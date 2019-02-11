using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class AssignmentTagValidation : AbstractValidator<string>
    {
        public AssignmentTagValidation()
        {
            RuleFor( m => m).NotEmpty().MinimumLength(3).MaximumLength(30);
        }
    }
}
