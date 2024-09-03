using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.Common;
using DTO.GetDTO;
using DTO.PostDTO;
using Repository.IRepository;


namespace Repository.Repository
{

    public class SemesterRepository : ISemesterRepository
    {

        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public SemesterRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddSemester(SemesterPostDTO semesterPostDTO)
        {
            if (semesterPostDTO == null)
            {
                throw new Exception(ErrorMessage.NULL);
            }
            try
            {
                SemesterDAO semesterDAO = new SemesterDAO(_context);
                return semesterDAO.AddSemester(_mapper.Map<Semester>(semesterPostDTO)) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<SemesterGerDTO> GetSemester(int? year)
        {
            SemesterDAO semesterDAO = new SemesterDAO(_context);
            return _mapper.Map<List<SemesterGerDTO>>(semesterDAO.GetSemester(year));
        }
    }
}
