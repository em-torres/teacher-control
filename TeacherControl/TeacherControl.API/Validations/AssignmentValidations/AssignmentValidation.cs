using FluentValidation;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class AssignmentValidation : AbstractValidator<AssignmentDTO>
    {
        public AssignmentValidation()
        {
            RuleFor(m => m.Title).NotEmpty().MinimumLength(15).MaximumLength(150);
            RuleFor(m => m.Body).NotEmpty().MinimumLength(50).MaximumLength(600);
            RuleFor(m => m.Points).NotEmpty().GreaterThan(0);

            RuleFor(m => m.Status).NotNull().InclusiveBetween(1, 3);
            RuleForEach(m => m.Tags).SetValidator(new AssignmentTagValidation());
            RuleForEach(m => m.Types).SetValidator(new AssignmentTypeValidation());
            RuleForEach(m => m.Groups).SetValidator(new AssignmentTypeValidation());
        }
    }
}
