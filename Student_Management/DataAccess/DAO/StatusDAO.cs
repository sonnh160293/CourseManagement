using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class StatusDAO
    {
        private readonly StudentManagementContext _context;
        public StatusDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Status> GetStatuses()
        {
            return _context.Statuses.Include(s => s.Type).ToList();
        }

        public List<Status> GetStatusByType(int typeId)
        {
            return _context.Statuses.Include(s => s.Type).Where(s => s.TypeId == typeId).ToList();
        }
    }
}
