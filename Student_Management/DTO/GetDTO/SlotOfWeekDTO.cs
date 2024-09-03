namespace DTO.GetDTO
{
    public class SlotOfWeekDTO
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public int DayOfWeekId { get; set; }

        public DayOfWeekDTO DayOfWeek { get; set; } = null!;
        public SlotGetDTO Slot { get; set; } = null!;
    }
}
