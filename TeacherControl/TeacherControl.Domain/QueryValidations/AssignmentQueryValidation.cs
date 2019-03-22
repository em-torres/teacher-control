using FluentValidation;
using System;
using TeacherControl.Core.Enums;
using TeacherControl.Core.Queries;

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

            RuleFor(m => m.Tags).NotEmpty().MinimumLength(5).MaximumLength(30); //TODO: put a regex here to valid \w+,\w+

            string DatesErrorMessage = "The Start Date should be less than the End Date value";
            RuleFor(m => DateTime.Compare(m.StartDate, m.EndDate) <= 0).Equal(true).WithMessage(DatesErrorMessage);
        }
    }
}
