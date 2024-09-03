namespace DTO.GetDTO
{
    public class SemesterGerDTO
    {
        public int SemesterId { get; set; }
        public string? SemesterName { get; set; }
        public int? Year { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
    }
}
