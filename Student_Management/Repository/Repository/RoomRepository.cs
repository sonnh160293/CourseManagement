using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;


namespace Repository.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public RoomRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<RoomGetDTO> GetRooms()
        {
            RoomDAO roomDAO = new RoomDAO(_context);
            return _mapper.Map<List<RoomGetDTO>>(roomDAO.GetRooms());
        }

        public List<RoomGetDTO> GetRoomsByBuilding(int buildingId)
        {
            RoomDAO roomDAO = new RoomDAO(_context);
            return _mapper.Map<List<RoomGetDTO>>(roomDAO.GetRoomsByBuildingId(buildingId));
        }
    }
}
