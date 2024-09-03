using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Status
    {
        public Status()
        {
            Courses = new HashSet<Course>();
            StudentCourseAttendances = new HashSet<StudentCourseAttendance>();
            StudentSubjectCurricula = new HashSet<StudentSubjectCurriculum>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int? TypeId { get; set; }

        public virtual Type? Type { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<StudentCourseAttendance> StudentCourseAttendances { get; set; }
        public virtual ICollection<StudentSubjectCurriculum> StudentSubjectCurricula { get; set; }
    }
}
