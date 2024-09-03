using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;
using DayOfWeek = BusinessObject.Models.DayOfWeek;

namespace Repository.Repository
{
    public class DayRepository : IDayRepository
    {
        private readonly StudentManagementContext _context;
        public DayRepository(StudentManagementContext context)
        {
            _context = context;
        }
        public List<DayOfWeek> GetDayOfWeeks()
        {
            DayOfWeekDAO dayOfWeekDAO = new DayOfWeekDAO(_context);
            return dayOfWeekDAO.GetDays();
        }


    }
}
