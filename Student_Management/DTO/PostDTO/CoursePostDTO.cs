namespace DTO.PostDTO
{
    public class CoursePostDTO
    {

        public int? TeacherId { get; set; }
        public int? SubjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public string? Description { get; set; }

        public int? CreatedBy { get; set; }
        public double? SlotPercentage { get; set; }
        public int? NumberOfStudent { get; set; }
        public string? CourseName { get; set; }
        public int? SemesterId { get; set; }
    }
}
