namespace DTO.PostDTO
{
    public class CourseSchedulePostDTO
    {
        public int? SlotId { get; set; }
        public int? RoomId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
    }
}
