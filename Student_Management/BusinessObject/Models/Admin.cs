using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Courses = new HashSet<Course>();
            Registrations = new HashSet<Registration>();
            Subjects = new HashSet<Subject>();
        }

        public int AdminId { get; set; }
        public string? Name { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
