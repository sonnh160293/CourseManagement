using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface IMajorRepository
    {
        public List<MajorGetDTO> GetMajors();

        public MajorGetDTO GetMajorById(int majorId);
    }
}
