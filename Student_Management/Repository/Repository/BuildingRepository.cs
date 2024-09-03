using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public BuildingRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BuildingGetDTO> GetBuildings()
        {
            BuildingDAO buildingDAO = new BuildingDAO(_context);
            return _mapper.Map<List<BuildingGetDTO>>(buildingDAO.GetBuildings());
        }
    }
}
