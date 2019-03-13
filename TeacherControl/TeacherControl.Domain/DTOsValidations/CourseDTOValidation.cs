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
            RuleFor(m => m.StartDate).NotNull().GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(m => m.EndDate).NotNull().LessThanOrEqualTo(DateTime.MaxValue);

            //RuleForEach(m => m.Tags).NotEmpty().MinimumLength(5).MaximumLength(30);
        }
    }
}
