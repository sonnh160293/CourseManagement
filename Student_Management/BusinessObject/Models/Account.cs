using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Account
    {
        public Account()
        {
            Admins = new HashSet<Admin>();
            StudentCourseAttendances = new HashSet<StudentCourseAttendance>();
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public int AccountId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public bool? StatusId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<StudentCourseAttendance> StudentCourseAttendances { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
