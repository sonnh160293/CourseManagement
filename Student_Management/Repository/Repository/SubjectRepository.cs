using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.Common;
using DTO.GetDTO;
using DTO.PostDTO;
using Repository.IRepository;


namespace Repository.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public SubjectRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddSubject(SubjectPostDTO subjectPostDTO)
        {
            if (subjectPostDTO == null)
            {
                throw new Exception(ErrorMessage.NULL);
            }

            SubjectDAO subjectDAO = new SubjectDAO(_context);
            if (subjectPostDTO.SubjectPrequisite.HasValue)
            {
                var subjectPrequisite = subjectDAO.GetSubjectById(subjectPostDTO.SubjectPrequisite.Value);
                if (subjectPrequisite.MajorId != subjectPostDTO.MajorId)
                {
                    throw new Exception(ErrorMessage.UNMATCH);

                }
                if (subjectPrequisite.Term > subjectPostDTO.Term)
                {
                    throw new Exception(ErrorMessage.INVALID);

                }
            }
            try
            {
                return subjectDAO.CreateSubject(_mapper.Map<Subject>(subjectPostDTO));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;

            }

        }

        public SubjectGetDTO GetSubjectById(int id)
        {
            SubjectDAO subjectDAO = new SubjectDAO(_context);
            return _mapper.Map<SubjectGetDTO>(subjectDAO.GetSubjectById(id));
        }

        public List<SubjectGetDTO> GetSubjects(int? majorId, int? term, int? adminId, string? subjectName, bool? status)
        {
            SubjectDAO subjectDAO = new SubjectDAO(_context);
            return _mapper.Map<List<SubjectGetDTO>>(subjectDAO.GetSubjects(majorId, term, adminId, subjectName, status));
        }

        public List<SubjectGetDTO> GetSubjectsPrequisite(int majorId, int term)
        {
            SubjectDAO subjectDAO = new SubjectDAO(_context);
            return _mapper.Map<List<SubjectGetDTO>>(subjectDAO.GetSubjectsPrequisite(majorId, term));
        }
    }
}
