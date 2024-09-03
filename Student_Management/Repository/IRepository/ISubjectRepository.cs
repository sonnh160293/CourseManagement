using DTO.GetDTO;
using DTO.PostDTO;

namespace Repository.IRepository
{
    public interface ISubjectRepository
    {
        public List<SubjectGetDTO> GetSubjects(int? majorId, int? term, int? adminId, string? subjectName, bool? status);
        public List<SubjectGetDTO> GetSubjectsPrequisite(int majorId, int term);
        public int AddSubject(SubjectPostDTO subjectPostDTO);

        public SubjectGetDTO GetSubjectById(int id);
    }
}
