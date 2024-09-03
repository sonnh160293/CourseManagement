using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class SubjectGrade
    {
        public SubjectGrade()
        {
            StudentCourseGrades = new HashSet<StudentCourseGrade>();
        }

        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public int? GradeCategoryId { get; set; }
        public string? GradeItem { get; set; }
        public double? Weight { get; set; }

        public virtual GradeCategory? GradeCategory { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<StudentCourseGrade> StudentCourseGrades { get; set; }
    }
}
