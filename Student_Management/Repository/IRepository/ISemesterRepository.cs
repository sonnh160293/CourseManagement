using DTO.GetDTO;
using DTO.PostDTO;
namespace Repository.IRepository
{
    public interface ISemesterRepository
    {
        public List<SemesterGerDTO> GetSemester(int? year);
        public bool AddSemester(SemesterPostDTO semesterPostDTO);
    }
}
