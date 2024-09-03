namespace DTO.GetDTO
{
    public class RoomGetDTO
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? BuildingId { get; set; }

        public BuildingGetDTO Building { get; set; } = null!;
    }
}
