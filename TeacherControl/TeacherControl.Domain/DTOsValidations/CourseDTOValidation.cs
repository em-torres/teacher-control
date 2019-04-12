using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;

namespace TeacherControl.Domain.DTOsValidations
{
    public class CourseDTOValidation : AbstractValidator<CourseDTO>
    {
        public CourseDTOValidation()
        {
            RuleFor(m => m.Name).NotEmpty().MinimumLength(5).MaximumLength(150);
            RuleFor(m => m.Description).NotEmpty().MinimumLength(5).MaximumLength(5000);
            RuleFor(m => m.Credits).NotEmpty().GreaterThan(0);
            RuleFor(m => m.CodeId).NotEmpty().MinimumLength(3).MaximumLength(15);
            RuleFor(m => m.StartDate).NotNull().GreaterThanOrEqualTo(DateTime.MinValue);
            RuleFor(m => m.EndDate).NotNull().LessThanOrEqualTo(DateTime.MaxValue);
            RuleFor(m => m.Professor).GreaterThan(0);
            RuleForEach(m => m.Tags).NotEmpty().MinimumLength(5).MaximumLength(30);

            string DatesErrorMessage = "The Start Date should be less than the End Date value";
            RuleFor(m => DateTime.Compare(m.StartDate, m.EndDate) <= 0).Equal(true).WithMessage(DatesErrorMessage);
        }
    }
}
