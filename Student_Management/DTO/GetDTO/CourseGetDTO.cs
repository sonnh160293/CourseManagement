namespace DTO.GetDTO
{
    public class CourseGetDTO
    {
        public int CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int? SubjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public double? SlotPercentage { get; set; }
        public int? NumberOfStudent { get; set; }
        public string? CourseName { get; set; }
        public int? SemesterId { get; set; }

        public AdminGetDTO? CreatedByNavigation { get; set; }
        public SemesterGerDTO? Semester { get; set; }
        public StatusGetDTO? StatusNavigation { get; set; }
        public SubjectGetDTO? Subject { get; set; }
        public TeacherGetDTO? Teacher { get; set; }
    }
}
