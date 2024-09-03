using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class RoomDAO
    {
        private readonly StudentManagementContext _context;

        public RoomDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Room> GetRooms()
        {
            return _context.Rooms.Include(r => r.Building).ToList();
        }

        public List<Room> GetRoomsByBuildingId(int id)
        {
            return _context.Rooms.Include(r => r.Building).Where(r => r.BuildingId == id).ToList();
        }
    }
}
