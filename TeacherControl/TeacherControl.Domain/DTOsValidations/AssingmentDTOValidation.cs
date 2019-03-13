using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;

namespace TeacherControl.Domain.DTOsValidations
{
    public class AssingmentDTOValidation : AbstractValidator<AssignmentDTO>
    {
        protected readonly string DatesErrorMessage;

        public AssingmentDTOValidation()
        {
            RuleFor(m => m.Title).NotEmpty().MinimumLength(5).MaximumLength(150);
            RuleFor(m => m.StartDate).NotNull().GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(m => m.EndDate).NotNull().LessThanOrEqualTo(DateTime.MaxValue);
            RuleFor(m => m.Body).NotEmpty().MaximumLength(5000);
            RuleFor(m => m.Points).GreaterThan(0);

            RuleForEach(m => m.Tags).NotEmpty().MinimumLength(5).MaximumLength(30);
            RuleForEach(m => m.Groups).NotEmpty().MinimumLength(5).MaximumLength(50);

            DatesErrorMessage = "The Start Date should be less than the End Date value";
            RuleFor(m => DateTime.Compare(m.StartDate, m.EndDate) <= 0).Equal(true).WithMessage(DatesErrorMessage);
        }

    }
}
