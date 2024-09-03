using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Courses = new HashSet<Course>();
        }

        public int SemesterId { get; set; }
        public string? SemesterName { get; set; }
        public int? Year { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
