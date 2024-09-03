using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Major
    {
        public Major()
        {
            Curricula = new HashSet<Curriculum>();
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
            Teachers = new HashSet<Teacher>();
        }

        public int MajorId { get; set; }
        public string? MajorCode { get; set; }
        public string? MajorName { get; set; }

        public virtual ICollection<Curriculum> Curricula { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
