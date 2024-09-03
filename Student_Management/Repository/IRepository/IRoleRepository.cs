using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface IRoleRepository
    {
        public List<RoleDTO> GetRoles();
    }
}
