namespace DTO.GetDTO
{
    public class CurriculumGetDTO
    {
        public int CurriculumId { get; set; }
        public string? CurriculumName { get; set; }
        public int? MajorId { get; set; }
        public bool? Status { get; set; }

        public MajorGetDTO? Major { get; set; }
    }
}
