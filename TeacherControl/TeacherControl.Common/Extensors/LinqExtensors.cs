using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeacherControl.Common.Extensors
{
    public static class LinqExtensors
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> entities, int page, int size = 50) where T : class
        {
            page = page > 0 ? page : 0;
            size = size > 0 ? size : 50;
            return entities.Skip(page * size).Take(size);
        }
    }
}
