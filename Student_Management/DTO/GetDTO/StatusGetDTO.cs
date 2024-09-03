namespace DTO.GetDTO
{
    public class StatusGetDTO
    {
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int? TypeId { get; set; }

        public TypeGetDTO? Type { get; set; }
    }
}
