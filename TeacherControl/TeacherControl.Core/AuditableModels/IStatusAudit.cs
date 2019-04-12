using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.Core.AuditableModels
{
    public interface IStatusAudit
    {
        string Status { get; set; }
    }
}
