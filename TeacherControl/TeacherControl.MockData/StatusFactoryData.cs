using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.MockData
{
    public static class StatusFactoryData
    {
        public static IEnumerable<Status> CreateUserList() => new List<Status>
        {
                new Status { Id = 1, Name = "Active", },
                new Status { Id = 2, Name = "InActive" },
                new Status { Id = 3, Name = "Pending" },
                new Status { Id = 4, Name = "Disabled" },
                new Status { Id = 5, Name = "Deleted" },
                new Status { Id = 6, Name = "Deprecated" },
                new Status { Id = 7, Name = "Blocked" }
        };

    }
}
