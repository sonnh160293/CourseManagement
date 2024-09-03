using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface ITeacherRepository
    {
        public List<TeacherGetDTO> GetTeachers(int? majorId);
        public TeacherGetDTO GetTeacherById(int id);
    }
}
