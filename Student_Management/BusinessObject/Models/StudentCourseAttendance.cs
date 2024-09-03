using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class StudentCourseAttendance
    {
        public int Id { get; set; }
        public int? StudentCourseId { get; set; }
        public int? CourseScheduleId { get; set; }
        public int? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        public virtual CourseSchedule? CourseSchedule { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Status? StatusNavigation { get; set; }
        public virtual StudentCourse? StudentCourse { get; set; }
    }
}
