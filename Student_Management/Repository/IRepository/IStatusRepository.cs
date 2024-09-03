using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface IStatusRepository
    {
        public List<StatusGetDTO> GetStatuses();
        public List<StatusGetDTO> GetStatusesByTypeId(int typeId);
    }
}
