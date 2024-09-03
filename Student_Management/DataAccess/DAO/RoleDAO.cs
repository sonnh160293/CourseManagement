using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class RoleDAO
    {
        private readonly StudentManagementContext _context;
        public RoleDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.RoleId == id);
        }
    }
}
