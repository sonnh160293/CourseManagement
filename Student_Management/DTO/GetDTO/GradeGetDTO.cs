namespace DTO.GetDTO
{
    public class GradeGetDTO
    {
        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public int? GradeCategoryId { get; set; }
        public string? GradeItem { get; set; }
        public double? Weight { get; set; }

        public GradeCategoryGetDTO? GradeCategory { get; set; }

    }
}
