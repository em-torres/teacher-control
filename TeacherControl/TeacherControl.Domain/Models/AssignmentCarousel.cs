using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class AssignmentCarousel : BaseModel
    {
        public int Order { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int UrlType { get; set; }
        public int AssignmentId { get; set; }

        public virtual Assignment Assignment { get; set; }

        public AssignmentCarousel(int order, string url, string title, int urlType, int assignmentId, Assignment assignment)
        {
            Order = order;
            Url = url;
            Title = title;
            UrlType = urlType;
            AssignmentId = assignmentId;
            Assignment = assignment;
        }

        public AssignmentCarousel()
        {
            Order = 0;
            Url = string.Empty;
            Title = string.Empty;
            UrlType = 0;
            AssignmentId = 0;
        }
    }
}
