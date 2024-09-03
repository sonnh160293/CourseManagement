using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class BuildingDAO
    {
        private readonly StudentManagementContext _context;

        public BuildingDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Building> GetBuildings()
        {
            return _context.Buildings.ToList();
        }
    }
}
