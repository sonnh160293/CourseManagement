using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseSchedules = new HashSet<CourseSchedule>();
            StudentCourseGrades = new HashSet<StudentCourseGrade>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int? SubjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public double? SlotPercentage { get; set; }
        public int? NumberOfStudent { get; set; }
        public string? CourseName { get; set; }
        public int? SemesterId { get; set; }

        public virtual Admin? CreatedByNavigation { get; set; }
        public virtual Semester? Semester { get; set; }
        public virtual Status? StatusNavigation { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
        public virtual ICollection<StudentCourseGrade> StudentCourseGrades { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
