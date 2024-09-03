using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class MajorRepository : IMajorRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;


        public MajorRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MajorGetDTO GetMajorById(int majorId)
        {
            MajorDAO majorDAO = new MajorDAO(_context);
            return _mapper.Map<MajorGetDTO>(majorDAO.GetMajorById(majorId));
        }

        public List<MajorGetDTO> GetMajors()
        {
            MajorDAO majorDAO = new MajorDAO(_context);
            return _mapper.Map<List<MajorGetDTO>>(majorDAO.GetMajors());
        }
    }
}
