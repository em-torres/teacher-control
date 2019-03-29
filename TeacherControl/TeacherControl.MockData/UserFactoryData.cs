using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.Enums;
using TeacherControl.Core.Models;

namespace TeacherControl.MockData
{
    public static class UserFactoryData
    {
        public static IEnumerable<User> CreateUserList(int howMany) => new Faker<User>()
            .RuleFor(i => i.Id, i => i.IndexVariable += 1)
            .RuleFor(i => i.Password, i => i.Internet.Password())
            .RuleFor(i => i.Email, i => i.Internet.Email())
            .RuleFor(i => i.Username, i => i.Internet.UserName())
            .Generate(howMany);

        public static IEnumerable<UserInfo> CreateUserInfoList(int howMany) => new Faker<UserInfo>()
            .RuleFor(i => i.Id, i => i.IndexVariable += 1)
            .RuleFor(i => i.UserId, i => i.IndexVariable + 1)
            .RuleFor(i => i.CodeId, i => Guid.NewGuid().ToString().Split("-").Last())
            .RuleFor(i => i.FirstName, i => i.Person.FirstName)
            .RuleFor(i => i.LastName, i => i.Person.LastName)
            .RuleFor(i => i.Gender, i => Gender.Male.ToString())
            .RuleFor(i => i.PhoneNumber, i => i.Phone.PhoneNumber().Split("x").First())
            .Generate(howMany);
    }
}
