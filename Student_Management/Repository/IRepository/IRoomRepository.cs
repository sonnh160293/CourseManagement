using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface IRoomRepository
    {
        public List<RoomGetDTO> GetRooms();
        public List<RoomGetDTO> GetRoomsByBuilding(int buildingId);
    }
}
