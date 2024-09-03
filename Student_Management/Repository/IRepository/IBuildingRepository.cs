using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface IBuildingRepository
    {
        public List<BuildingGetDTO> GetBuildings();
    }
}
