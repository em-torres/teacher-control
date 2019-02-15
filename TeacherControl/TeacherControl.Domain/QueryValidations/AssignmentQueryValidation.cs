using FluentValidation;
using System;
using TeacherControl.Domain.Enums;
using TeacherControl.Domain.Queries;

namespace TeacherControl.Domain.QueryValidations
{
    public class AssignmentQueryValidation : AbstractValidator<AssignmentQuery>
    {
        public AssignmentQueryValidation()
        {
            RuleFor(m => m.Title).NotEmpty().MinimumLength(5).MaximumLength(150);
            RuleFor(m => m.StartDate).NotNull().GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(m => m.EndDate).NotNull().LessThanOrEqualTo(DateTime.MaxValue);
            RuleFor(m => m.Status).IsInEnum();
            RuleFor(m => m.Points).GreaterThan(0);

            RuleFor(m => m.Tags).NotEmpty().MinimumLength(5).MaximumLength(30);
            RuleFor(m => m.Groups).NotEmpty().MinimumLength(5).MaximumLength(50);

            string DatesErrorMessage = "The Start Date should be less than the End Date value";
            RuleFor(m => DateTime.Compare(m.StartDate, m.EndDate) <= 0).Equal(true).WithMessage(DatesErrorMessage);
        }
    }
}
