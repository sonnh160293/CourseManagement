using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.Common;
using DTO.GetDTO;
using DTO.PostDTO;
using Repository.IRepository;


namespace Repository.Repository
{
    public class CurriculumRepository : ICurriculumRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public CurriculumRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddCurrculum(CurriculumPostDTO curriculum)
        {
            if (curriculum == null)
            {
                return false;
            }
            try
            {
                CurriculumDAO curriculumDAO = new CurriculumDAO(_context);
                return curriculumDAO.AddCurriculum(_mapper.Map<Curriculum>(curriculum)) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool AddSubjecsCurriculum(List<SubjectCurriculumPostDTO> curriculumList)
        {
            if (curriculumList == null)
            {
                return false;
            }
            try
            {
                CurriculumDAO curriculumDao = new CurriculumDAO(_context);
                return curriculumDao.AddSubjectsToCurriculum(_mapper.Map<List<SubjectCurriculum>>(curriculumList)) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool AddSubjectCurriculum(SubjectCurriculumPostDTO curriculum)
        {
            if (curriculum == null)
            {
                throw new ArgumentNullException(ErrorMessage.NULL);
            }

            CurriculumDAO curriculumDao = new CurriculumDAO(_context);
            SubjectDAO subjectDAO = new SubjectDAO(_context);

            if (curriculumDao.GetSubjectInCurriculum((int)curriculum.SubjectId, (int)curriculum.CurriculumId) != null)
            {
                throw new Exception(ErrorMessage.DUPLICATE);
            }

            var curriculumObj = curriculumDao.GetCurriculumById((int)curriculum.CurriculumId);
            var subjectObj = subjectDAO.GetSubjectById((int)curriculum.SubjectId);

            if (curriculumObj.MajorId != subjectObj.MajorId)
            {
                throw new Exception(ErrorMessage.UNMATCH);
            }

            if (subjectObj.Status == false)
            {
                throw new Exception(ErrorMessage.NOT_ACTIVE);
            }

            try
            {
                return curriculumDao.AddSubjectToCurriculum(_mapper.Map<SubjectCurriculum>(curriculum)) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Database error: Failed to add subject to curriculum", ex);
            }
        }


        public CurriculumGetDTO GetCurriculumById(int id)
        {
            CurriculumDAO curriculumDAO = new CurriculumDAO(_context);
            return _mapper.Map<CurriculumGetDTO>(curriculumDAO.GetCurriculumById(id));
        }

        public List<CurriculumGetDTO> GetCurriculums(int? majorId)
        {
            CurriculumDAO curriculumDAO = new CurriculumDAO(_context);
            return _mapper.Map<List<CurriculumGetDTO>>(curriculumDAO.GetCurriculums(majorId));
        }

        public List<SubjectCurriculumGetDTO> GetSubjectsInCurriculum(int curriculumId)
        {
            CurriculumDAO curriculumDAO = new CurriculumDAO(_context);
            return _mapper.Map<List<SubjectCurriculumGetDTO>>(curriculumDAO.GetSubjectsInCurriculum(curriculumId));
        }
    }
}
