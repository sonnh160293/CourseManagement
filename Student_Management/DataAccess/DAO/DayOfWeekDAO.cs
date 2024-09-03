using BusinessObject.Models;
using DayOfWeek = BusinessObject.Models.DayOfWeek;

namespace DataAccess.DAO
{
    public class DayOfWeekDAO
    {
        private readonly StudentManagementContext _context;
        public DayOfWeekDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<DayOfWeek> GetDays()
        {
            return _context.DayOfWeeks.ToList();
        }


    }
}
