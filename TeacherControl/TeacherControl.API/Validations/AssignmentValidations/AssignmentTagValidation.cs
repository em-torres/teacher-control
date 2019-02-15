using FluentValidation;

namespace TeacherControl.API.Validations
{
    public class AssignmentTagValidation : AbstractValidator<string>
    {
        public AssignmentTagValidation()
        {
            RuleFor( m => m).NotEmpty().Matches(@"[\w+-#]{3,30}");
        }
    }
}
