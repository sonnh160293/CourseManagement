using DTO.GetDTO;
using DTO.PostDTO;
namespace Repository.IRepository
{
    public interface IAdminRepository
    {
        public List<AdminGetDTO> GetAdmins();
        public AdminGetDTO GetAdminById(int id);
        public bool AddAdmin(AdminPostDTO adminRequestDTO);
    }
}
