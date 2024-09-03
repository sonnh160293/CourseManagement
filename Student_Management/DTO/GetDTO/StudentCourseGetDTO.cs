namespace DTO.GetDTO
{
    public class StudentCourseGetDTO
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }

        public virtual CourseGetDTO? Course { get; set; }
        public virtual StudentGetDTO? Student { get; set; }
    }
}
