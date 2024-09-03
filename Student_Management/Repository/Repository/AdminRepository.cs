using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using DTO.PostDTO;
using Repository.IRepository;


namespace Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMapper _mapper;
        private readonly StudentManagementContext _context;

        public AdminRepository(IMapper mapper, StudentManagementContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public bool AddAdmin(AdminPostDTO adminRequestDTO)
        {
            if (adminRequestDTO == null)
            {
                return false;
            }
            try
            {
                AdminDAO adminDao = new AdminDAO(_context);
                return adminDao.AddAdmin(_mapper.Map<Admin>(adminRequestDTO)) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public AdminGetDTO GetAdminById(int id)
        {
            AdminDAO adminDao = new AdminDAO(_context);
            return _mapper.Map<AdminGetDTO>(adminDao.GetAdminById(id));
        }

        public List<AdminGetDTO> GetAdmins()
        {
            AdminDAO adminDao = new AdminDAO(_context);
            return _mapper.Map<List<AdminGetDTO>>(adminDao.GetAdmins());
        }
    }
}
