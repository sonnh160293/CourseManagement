using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Curriculum
    {
        public Curriculum()
        {
            Students = new HashSet<Student>();
            SubjectCurricula = new HashSet<SubjectCurriculum>();
        }

        public int CurriculumId { get; set; }
        public string? CurriculumName { get; set; }
        public int? MajorId { get; set; }
        public bool? Status { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<SubjectCurriculum> SubjectCurricula { get; set; }
    }
}
