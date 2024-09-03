using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class MajorDAO
    {
        private readonly StudentManagementContext _context;

        public MajorDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Major> GetMajors()
        {
            return _context.Majors.ToList();
        }

        public Major GetMajorById(int id)
        {
            return _context.Majors.FirstOrDefault(m => m.MajorId == id);
        }
    }
}
