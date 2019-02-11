using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.API.Validations
{
    public class GroupValidator : AbstractValidator<GroupDTO>
    {
        public GroupValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MinimumLength(15).MaximumLength(150);
            RuleFor(m => m.Status).InclusiveBetween(1, 10);//TODO: how many status do i have?

            RuleForEach(m => m.Privileges).SetValidator(new GroupPrivilegeValidator());
        }
    }
}
