namespace DTO.GetDTO
{
    public class TeacherGetDTO
    {
        public int TeacherId { get; set; }
        public string? Name { get; set; }
        public int? MajorId { get; set; }
        public int? AccountId { get; set; }
        public string? TeacherCode { get; set; }

        public AccountGetDTO? Account { get; set; }
        public MajorGetDTO? Major { get; set; }
    }
}
