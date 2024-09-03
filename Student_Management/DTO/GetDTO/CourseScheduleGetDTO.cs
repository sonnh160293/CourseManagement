namespace DTO.GetDTO
{
    public class CourseScheduleGetDTO
    {
        public int Id { get; set; }
        public int? SlotId { get; set; }
        public int? RoomId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }

        public CourseGetDTO? Course { get; set; }
        public RoomGetDTO? Room { get; set; }
        public SlotOfWeekDTO? SlotOfWeek { get; set; }
    }
}
