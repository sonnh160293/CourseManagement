namespace BusinessObject.Models
{
    public partial class CourseSchedule
    {
        public CourseSchedule()
        {
            StudentCourseAttendances = new HashSet<StudentCourseAttendance>();
        }

        public int Id { get; set; }
        public int? SlotId { get; set; }
        public int? RoomId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Room? Room { get; set; }
        public virtual SlotOfWeek? SlotOfWeek { get; set; }
        public virtual ICollection<StudentCourseAttendance> StudentCourseAttendances { get; set; }
    }
}
