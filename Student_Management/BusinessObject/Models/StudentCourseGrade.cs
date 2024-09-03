using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class StudentCourseGrade
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? SubjectGradeId { get; set; }
        public double? Value { get; set; }
        public string? Comment { get; set; }
        public int? Status { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
        public virtual SubjectGrade? SubjectGrade { get; set; }
    }
}
