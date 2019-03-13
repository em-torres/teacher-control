using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.AuditableModels
{
    public interface  IModificationAudit
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
