using FluentValidation;
using TeacherControl.Common.Enums;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class AssignmentValidation : AbstractValidator<AssignmentDTO>
    {
        public AssignmentValidation()
        {
            RuleFor(m => m.Title).NotEmpty().MinimumLength(12).MaximumLength(150);
            RuleFor(m => m.Body).NotEmpty().MinimumLength(50).MaximumLength(600);
            RuleFor(m => m.Points).NotEmpty().GreaterThan(0);
            RuleFor(m => m.StartDate).NotNull().LessThanOrEqualTo(m => m.EndDate);
            RuleFor(m => m.EndDate).NotNull().GreaterThanOrEqualTo(m => m.StartDate);
            RuleFor(m => m.Points).NotEmpty().GreaterThan(0);

            RuleFor(m => m.Status).NotNull().IsInEnum();
            RuleForEach(m => m.Tags).SetValidator(new AssignmentTagValidation());
            RuleForEach(m => m.Groups).SetValidator(new AssignmentGroupValidator());
        }
    }
}
