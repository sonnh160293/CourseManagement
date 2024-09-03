using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public StatusRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<StatusGetDTO> GetStatuses()
        {
            StatusDAO statusDAO = new StatusDAO(_context);
            return _mapper.Map<List<StatusGetDTO>>(statusDAO.GetStatuses());
        }

        public List<StatusGetDTO> GetStatusesByTypeId(int typeId)
        {
            StatusDAO statusDAO = new StatusDAO(_context);
            return _mapper.Map<List<StatusGetDTO>>(statusDAO.GetStatusByType(typeId));
        }
    }
}
