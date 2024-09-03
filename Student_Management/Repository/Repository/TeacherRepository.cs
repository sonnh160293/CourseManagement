using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;
        public TeacherRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<TeacherGetDTO> GetTeachers(int? majorId)
        {
            TeacherDAO teacherDAO = new TeacherDAO(_context);
            return _mapper.Map<List<TeacherGetDTO>>(teacherDAO.GetTeachers(majorId));
        }

        public TeacherGetDTO GetTeacherById(int id)
        {
            TeacherDAO teacherDAO = new TeacherDAO(_context);
            return _mapper.Map<TeacherGetDTO>(teacherDAO.GetTeacherById(id));
        }
    }
}
