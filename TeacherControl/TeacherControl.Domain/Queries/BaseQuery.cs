using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Queries
{
    public class BaseQuery
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
