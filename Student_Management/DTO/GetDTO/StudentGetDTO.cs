namespace DTO.GetDTO
{
    public class StudentGetDTO
    {
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? Name { get; set; }
        public int? CurrentTerm { get; set; }
        public string? Phone { get; set; }
        public int? MajorId { get; set; }
        public int? AccountId { get; set; }
        public int? CurriculumId { get; set; }

        public AccountGetDTO? Account { get; set; }
        public CurriculumGetDTO? Curriculum { get; set; }
        public MajorGetDTO? Major { get; set; }
    }
}
