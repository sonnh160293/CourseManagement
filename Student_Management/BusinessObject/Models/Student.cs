using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourseGrades = new HashSet<StudentCourseGrade>();
            StudentCourses = new HashSet<StudentCourse>();
            StudentSubjectCurricula = new HashSet<StudentSubjectCurriculum>();
        }

        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? Name { get; set; }
        public int? CurrentTerm { get; set; }
        public string? Phone { get; set; }
        public int? MajorId { get; set; }
        public int? AccountId { get; set; }
        public int? CurriculumId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Curriculum? Curriculum { get; set; }
        public virtual Major? Major { get; set; }
        public virtual ICollection<StudentCourseGrade> StudentCourseGrades { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentSubjectCurriculum> StudentSubjectCurricula { get; set; }
    }
}
