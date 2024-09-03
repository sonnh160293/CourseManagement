namespace DTO.GetDTO
{
    public class SubjectCurriculumGetDTO
    {
        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public int? CurriculumId { get; set; }
        public bool? Status { get; set; }


        public virtual CurriculumGetDTO? Curriculum { get; set; }
        public virtual SubjectPrequisiteDTO? Subject { get; set; }
    }
}
