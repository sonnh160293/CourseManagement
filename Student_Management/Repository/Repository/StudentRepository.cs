using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {

        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;
        public StudentRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StudentGetDTO GetStudentById(int id)
        {
            StudentDAO studentDAO = new StudentDAO(_context);
            return _mapper.Map<StudentGetDTO>(studentDAO.GetStudentById(id));
        }

        public List<StudentGetDTO> GetStudents(int? majorId, string? studentCode, int? currentTerm)
        {
            StudentDAO studentDAO = new StudentDAO(_context);
            return _mapper.Map<List<StudentGetDTO>>(studentDAO.GetStudents(majorId, studentCode, currentTerm));
        }
    }
}
