using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
            InverseSubjectPrequisiteNavigation = new HashSet<Subject>();
            SubjectCurricula = new HashSet<SubjectCurriculum>();
            SubjectGrades = new HashSet<SubjectGrade>();
        }

        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public int? NumOfCredit { get; set; }
        public int? SubjectPrequisite { get; set; }
        public int? MajorId { get; set; }
        public int? Term { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Admin? CreatedByNavigation { get; set; }
        public virtual Major? Major { get; set; }
        public virtual Subject? SubjectPrequisiteNavigation { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Subject> InverseSubjectPrequisiteNavigation { get; set; }
        public virtual ICollection<SubjectCurriculum> SubjectCurricula { get; set; }
        public virtual ICollection<SubjectGrade> SubjectGrades { get; set; }
    }
}
