﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.MockData
{
    public static class AssignmentFactoryData
    {
        public static IEnumerable<Assignment> CreateAssignmentList(int howMany) => new Faker<Assignment>()
            .RuleFor(i => i.Id, i => i.IndexFaker + 1)
            .RuleFor(i => i.Title, i => $"TEST {i.Lorem.Sentence()}")
            .RuleFor(i => i.HashIndex, i => string.Join("", Guid.NewGuid().ToString().Split("-")).Substring(0, 12))
            .RuleFor(i => i.StartDate, i => i.Date.Recent(2))
            .RuleFor(i => i.EndDate, i => i.Date.Soon(2))
            .RuleFor(i => i.Body, i => i.Lorem.Paragraph())
            .RuleFor(i => i.Points, i => i.Random.Double())
            .Generate(howMany);

        public static IEnumerable<AssignmentCounts> CreateAssignmentCountList(int howMany) => new Faker<AssignmentCounts>()
            .RuleFor(i => i.Id, i => i.IndexFaker + 1)
            .RuleFor(i => i.UpvotesCount, i => i.Random.Number(1, 10000))
            .RuleFor(i => i.ViewsCount, i => i.Random.Number(1, 10000))
            .Generate(howMany);

        public static IEnumerable<AssignmentTag> CreateCourseTagList(int howMany) => new Faker<AssignmentTag>()
            .RuleFor(i => i.Id, i => i.IndexFaker + 1)
            .RuleFor(i => i.Name, i => i.Lorem.Word())
            .Generate(howMany);
    }
}