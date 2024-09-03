using DTO.GetDTO;
using DTO.PostDTO;

namespace Repository.IRepository
{
    public interface ICurriculumRepository
    {
        public List<CurriculumGetDTO> GetCurriculums(int? majorId);
        public CurriculumGetDTO GetCurriculumById(int id);

        public bool AddCurrculum(CurriculumPostDTO curriculum);

        public List<SubjectCurriculumGetDTO> GetSubjectsInCurriculum(int curriculumId);

        public bool AddSubjectCurriculum(SubjectCurriculumPostDTO curriculum);
        public bool AddSubjecsCurriculum(List<SubjectCurriculumPostDTO> curriculumList);
    }
}
