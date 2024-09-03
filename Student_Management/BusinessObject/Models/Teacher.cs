namespace BusinessObject.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public string? Name { get; set; }
        public int? MajorId { get; set; }
        public int? AccountId { get; set; }
        public string? TeacherCode { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Major? Major { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
