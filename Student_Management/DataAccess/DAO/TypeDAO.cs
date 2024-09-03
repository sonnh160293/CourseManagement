using BusinessObject.Models;
using Type = BusinessObject.Models.Type;

namespace DataAccess.DAO
{
    public class TypeDAO
    {
        private readonly StudentManagementContext _context;
        public TypeDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Type> GetTypes()
        {
            return _context.Types.ToList();
        }
    }
}
