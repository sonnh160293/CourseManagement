using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface ITypeRepository
    {
        public List<TypeGetDTO> GetTypes();
    }
}
