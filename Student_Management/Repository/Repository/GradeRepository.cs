using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using DTO.PostDTO;
using DTO.UpdateDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;
        public GradeRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddGradesToSubject(List<GradeSubjecPostDTO> subjectGrades)
        {
            if (subjectGrades == null)
            {
                return 0;
            }
            GradeDAO gradeDAO = new GradeDAO(_context);
            try
            {
                return gradeDAO.AddGradesToSubject(_mapper.Map<List<SubjectGrade>>(subjectGrades));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<GradeGetDTO> GetGradesOfSubject(int subjectId)
        {
            GradeDAO gradeDAO = new GradeDAO(_context);
            return _mapper.Map<List<GradeGetDTO>>(gradeDAO.GetGradesOfSubject(subjectId));
        }

        public bool UpdateGradesOfSubject(List<GradeUpdateDTO> grades)
        {
            if (grades == null)
            {
                return false;
            }

            GradeDAO gradeDAO = new GradeDAO(_context);
            try
            {
                if (gradeDAO.UpdateGradesOfSubject(_mapper.Map<List<SubjectGrade>>(grades)) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
