using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class GradeCategory
    {
        public GradeCategory()
        {
            SubjectGrades = new HashSet<SubjectGrade>();
        }

        public int Id { get; set; }
        public string? GradeCategoryName { get; set; }

        public virtual ICollection<SubjectGrade> SubjectGrades { get; set; }
    }
}
