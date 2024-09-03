namespace DTO.PostDTO
{
    public class SubjectPostDTO
    {
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public int? NumOfCredit { get; set; }
        public int? SubjectPrequisite { get; set; }
        public int? MajorId { get; set; }
        public int? Term { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
    }
}
