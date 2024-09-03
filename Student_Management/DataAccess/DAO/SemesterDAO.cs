using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class SemesterDAO
    {

        private readonly StudentManagementContext _context;
        public SemesterDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Semester> GetSemester(int? year)
        {
            var semester = _context.Semesters.ToList();
            if (year != null && year != 0)
            {
                semester = semester.Where(x => x.Year == year).ToList();
            }
            return semester;
        }

        public int AddSemester(Semester semester)
        {
            if (semester == null)
            {
                return 0;
            }
            try
            {
                _context.Semesters.Add(semester);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
