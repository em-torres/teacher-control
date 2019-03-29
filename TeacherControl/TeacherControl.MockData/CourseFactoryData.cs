using Bogus;
using System;
using System.Collections.Generic;
using TeacherControl.Core.Enums;
using TeacherControl.Core.Models;

namespace TeacherControl.MockData
{
    public static class CourseFactoryData
    {
        public static IEnumerable<Course> CreateCourseList(int howMany) => new Faker<Course>()
            .RuleFor(i => i.Id, i => i.IndexVariable += 1)
            .RuleFor(i => i.Name, i => $"TEST {string.Join("", i.Lorem.Words(3))}")
            .RuleFor(i => i.StartDate, i => i.Date.Recent(2))
            .RuleFor(i => i.EndDate, i => i.Date.Soon(2))
            .RuleFor(i => i.CodeId, i => Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12))
            .RuleFor(i => i.Credits, i => i.Random.Int(1, 100))
            .RuleFor(i => i.Description, i => i.Lorem.Paragraph())
            .RuleFor(i => i.CreatedBy, i => $"TEST {i.Internet.UserName()}")
            .RuleFor(i => i.CreatedDate, i => i.Date.Recent(3))
            .RuleFor(i => i.Status, i => Status.Active.ToString())
            .Generate(howMany);

        public static IEnumerable<CourseTag> CreateCourseTagList(int howMany) => new Faker<CourseTag>()
            .RuleFor(i => i.Id, i => i.IndexVariable += 1)
            .RuleFor(i => i.Name, i => i.Lorem.Word())
            .Generate(howMany);

        
    }
}
