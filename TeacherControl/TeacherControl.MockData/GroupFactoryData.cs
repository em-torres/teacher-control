using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.MockData
{
    public static class GroupFactoryData
    {
        public static IEnumerable<Group> CreateGroupList(int howMany) => new Faker<Group>()
            .RuleFor(i => i.Id, i => i.IndexVariable += 1)
            .RuleFor(i => i.Name, i => i.Lorem.Text())
            .RuleFor(i => i.CreatedBy, i => $"TEST {i.Internet.UserName()}")
            .RuleFor(i => i.CreatedDate, i => i.Date.Recent(3))
            .Generate(howMany);
    }
}
