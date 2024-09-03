using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class SubjectCurriculum
    {
        public SubjectCurriculum()
        {
            StudentSubjectCurricula = new HashSet<StudentSubjectCurriculum>();
        }

        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public int? CurriculumId { get; set; }
        public int? Term { get; set; }
        public bool? Status { get; set; }

        public virtual Curriculum? Curriculum { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<StudentSubjectCurriculum> StudentSubjectCurricula { get; set; }
    }
}
