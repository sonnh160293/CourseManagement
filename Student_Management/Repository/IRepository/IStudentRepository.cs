using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface IStudentRepository
    {
        public List<StudentGetDTO> GetStudents(int? majorId, string? studentCode, int? currentTerm);
        public StudentGetDTO GetStudentById(int id);
    }
}
