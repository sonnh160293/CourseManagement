using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class TeacherDAO
    {
        private readonly StudentManagementContext _context;

        public TeacherDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Teacher> GetTeachers(int? majorId)
        {
            var teachers = _context.Teachers.Include(t => t.Major).Include(t => t.Account).ToList();
            if (majorId != null && majorId > 0)
            {
                teachers = teachers.Where(s => s.MajorId == majorId).ToList();
            }
            return teachers;
        }

        public int AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            return _context.SaveChanges();
        }

        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.Include(t => t.Major).Include(t => t.Account).FirstOrDefault(t => t.TeacherId == id);
        }
    }
}
