using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.API.Validations
{
    public class GroupPrivilegeValidator : AbstractValidator<string>
    {
        public GroupPrivilegeValidator()
        {
            RuleFor(m => m).NotEmpty().MaximumLength(50).MinimumLength(5);
        }
    }
}
