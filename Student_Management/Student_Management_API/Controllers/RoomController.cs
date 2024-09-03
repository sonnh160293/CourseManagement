using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomRepository.GetRooms();
            if (rooms.Count == 0)
            {
                return NotFound();
            }
            return Ok(rooms);
        }


        [HttpGet("{buildingId}")]
        public IActionResult GetRoomsByBuilding(int buildingId)
        {
            var rooms = _roomRepository.GetRoomsByBuilding(buildingId);
            if (rooms.Count == 0)
            {
                return NotFound();
            }
            return Ok(rooms);
        }
    }
}
