﻿using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class Course : IModificationAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeIdentifier { get; set; }
        public string HashIndex { get; set; }
        public string Description { get; set; }
        public double Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<UserCourse> Professors { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<CourseComment> Comments { get; set; }
        public virtual ICollection<CourseTag> Tags { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}