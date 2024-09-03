using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;


namespace Repository.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly StudentManagementContext _context;

        public RoleRepository(IMapper mapper, StudentManagementContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<RoleDTO> GetRoles()
        {
            RoleDAO roleDAO = new RoleDAO(_context);
            return _mapper.Map<List<RoleDTO>>(roleDAO.GetRoles());
        }
    }
}
