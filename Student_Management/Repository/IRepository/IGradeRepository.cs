using DTO.GetDTO;
using DTO.PostDTO;
using DTO.UpdateDTO;

namespace Repository.IRepository
{
    public interface IGradeRepository
    {
        public List<GradeGetDTO> GetGradesOfSubject(int subjectId);
        public bool UpdateGradesOfSubject(List<GradeUpdateDTO> grades);

        public int AddGradesToSubject(List<GradeSubjecPostDTO> subjectGrades);
    }
}
