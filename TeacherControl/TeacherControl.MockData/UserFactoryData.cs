using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.MockData
{
    public static class UserFactoryData
    {
        public static IEnumerable<User> CreateUserList(int howMany) => new Faker<User>()
            .RuleFor(i => i.Id, i => i.IndexFaker + 1)
            .RuleFor(i => i.Email, i => i.Internet.Email())
            .RuleFor(i => i.Password, i => i.Internet.Password())
            .RuleFor(i => i.Username, i => i.Internet.UserName())
            .RuleFor(i => i.AuthToken, i =>  string.Join("", Guid.NewGuid().ToString().Split('-')))
            .Generate(howMany);

    }
}
