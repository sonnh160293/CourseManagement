using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class StudentSubjectCurriculum
    {
        public int Id { get; set; }
        public int? SubjectCurriculumId { get; set; }
        public int? StudentId { get; set; }
        public double? Grade { get; set; }
        public int? Status { get; set; }

        public virtual Status? StatusNavigation { get; set; }
        public virtual Student? Student { get; set; }
        public virtual SubjectCurriculum? SubjectCurriculum { get; set; }
    }
}
