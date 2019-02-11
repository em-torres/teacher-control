using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Repositories;
using TeacherControl.Domain.Models;
using TeacherControl.DataEFCore;
using TeacherControl.Domain.DTOs;
using AutoMapper;

namespace TeacherControl.DataEFCore.Repositories
{
    public class CourseRepository : BaseRepository<CourseDTO>, ICourseRepository
    {
        public CourseRepository(TCContext tCContext, IMapper mapper) : base(tCContext, mapper)
        {

        }
    }
}
