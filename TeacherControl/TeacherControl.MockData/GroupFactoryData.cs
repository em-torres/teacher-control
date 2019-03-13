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
            .RuleFor(i => i.Id, i => i.IndexFaker + 1)
            .RuleFor(i => i.Name, i => i.Lorem.Text())
            .Generate(howMany);
    }
}
