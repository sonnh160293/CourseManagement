using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class StudentDAO
    {
        private readonly StudentManagementContext _context;
        public StudentDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Student> GetStudents(int? majorId, string? studentCode, int? currentTerm)
        {
            var students = _context.Students.Include(s => s.Major).Include(s => s.StudentCourses)
                .Include(s => s.StudentCourseGrades)
                .Include(s => s.StudentSubjectCurricula)
                .Include(s => s.Account)
                .Include(s => s.Curriculum)
                .ToList();
            if (majorId != null && majorId > 0)
            {
                students = students.Where(s => s.MajorId == majorId).ToList();
            }
            if (!string.IsNullOrEmpty(studentCode))
            {
                students = students.Where(s => s.StudentCode.ToLower().Contains(studentCode.ToLower())).ToList();
            }
            if (currentTerm != null && currentTerm > 0)
            {
                students = students.Where((s) => s.CurrentTerm == currentTerm).ToList();
            }
            return students;
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.Include(s => s.Major).Include(s => s.StudentCourses)
                .Include(s => s.StudentCourseGrades)
                .Include(s => s.StudentSubjectCurricula)
                .Include(s => s.Account)
                .FirstOrDefault(s => s.StudentId == id);
        }

        public int AddStudent(Student student)
        {
            _context.Add(student);
            return _context.SaveChanges();
        }

        //public List<Student> GetStudentForCourse(Course course)
        //{
        //    var allStudent = _context.Students.Include(s => s.Major).Include(s => s.StudentCourses)
        //        .Include(s => s.StudentCourseGrades)
        //        .Include(s => s.StudentSubjectCurricula)
        //        .Include(s => s.Account)
        //        .Include(s => s.Curriculum)
        //        .ToList();

        //    var subjectInCourse = course.Subject;

        //    var availableStudent = allStudent.Where(s => s.CurrentTerm >= subjectInCourse.Term && s.MajorId == subjectInCourse.MajorId).ToList();
        //}
    }
}
