using FluentValidation;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class CourseValidator : AbstractValidator<CourseDTO>
    {
        public CourseValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MinimumLength(15).MaximumLength(60);
            RuleFor(m => m.Description).NotEmpty().MinimumLength(50).MaximumLength(300);
            RuleFor(m => m.Credits).NotNull().GreaterThan(0);
            RuleFor(m => m.StartDate).NotEmpty().LessThan(m => m.EndDate);
            RuleFor(m => m.EndDate).NotEmpty().GreaterThan(m => m.StartDate);

            RuleFor(m => m.Professor).NotNull().GreaterThan(0);
            RuleForEach(m => m.Tags).SetValidator(new CourseTagValidation());
            RuleForEach(m => m.Types).SetValidator(new CourseTypeValidation());
            RuleFor(m => m.Status).NotNull().InclusiveBetween(1, 3); //check TC.Common
        }
    }
}
