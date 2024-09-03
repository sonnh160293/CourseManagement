using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class StudentCourse
    {
        public StudentCourse()
        {
            StudentCourseAttendances = new HashSet<StudentCourseAttendance>();
        }

        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<StudentCourseAttendance> StudentCourseAttendances { get; set; }
    }
}
