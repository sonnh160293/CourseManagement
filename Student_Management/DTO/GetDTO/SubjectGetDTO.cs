namespace DTO.GetDTO
{

    public class SubjectGetDTO
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public int? NumOfCredit { get; set; }
        public int? SubjectPrequisite { get; set; }
        public int? MajorId { get; set; }
        public int? Term { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }

        public virtual AdminGetDTO? CreatedByNavigation { get; set; }
        public virtual MajorGetDTO? Major { get; set; }
        public virtual SubjectPrequisiteDTO? SubjectPrequisiteNavigation { get; set; }
    }
}
