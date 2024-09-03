using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class SubjectDAO
    {
        private readonly StudentManagementContext _context;
        public SubjectDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Subject> GetSubjects()
        {
            return _context.Subjects.Include(s => s.Major)
                .Include(s => s.CreatedByNavigation).
                Include(s => s.InverseSubjectPrequisiteNavigation).
                Where(s => s.Status == true).ToList();
        }

        public List<Subject> GetSubjectsByMajorId(int majorId)
        {
            return _context.Subjects.Include(s => s.Major)
                .Include(s => s.CreatedByNavigation).
                Include(s => s.InverseSubjectPrequisiteNavigation).
                Where(s => s.Status == true).
                Where(s => s.MajorId == majorId).ToList();
        }




        public List<Subject> GetSubjects(int? majorId, int? term, int? adminId, string? subjectName, bool? status)
        {
            List<Subject> subjects = _context.Subjects.Include(s => s.Major)
                .Include(s => s.CreatedByNavigation)
                .Include(s => s.InverseSubjectPrequisiteNavigation)
                .Include(s => s.SubjectPrequisiteNavigation)
                .ToList();
            if (majorId != null && majorId > 0)
            {
                subjects = subjects.Where(s => s.MajorId == majorId).ToList();
            }
            if (term != null && term > 0)
            {
                subjects = subjects.Where(s => s.Term == term).ToList();
            }
            if (adminId != null && adminId > 0)
            {
                subjects = subjects.Where(s => s.CreatedBy == adminId).ToList();
            }
            if (!String.IsNullOrEmpty(subjectName))
            {
                subjects = subjects.Where(s => s.SubjectName.ToLower().Contains(subjectName.ToLower())).ToList();
            }
            if (status != null)
            {
                subjects = subjects.Where(s => s.Status == status).ToList();
            }
            return subjects;
        }


        public List<Subject> GetSubjectsPrequisite(int majorId, int term)
        {
            List<Subject> subjects = _context.Subjects.Include(s => s.Major)

                .Where(s => s.Status == true).ToList();
            if (majorId != null && majorId > 0)
            {
                subjects = subjects.Where(s => s.MajorId == majorId).ToList();
            }
            if (term != null && term > 0)
            {
                subjects = subjects.Where(s => s.Term < term).ToList();
            }

            return subjects;
        }

        public Subject GetSubjectById(int id)
        {
            return _context.Subjects.Include(s => s.Major)
                .Include(s => s.CreatedByNavigation)
                .Include(s => s.InverseSubjectPrequisiteNavigation)
                .FirstOrDefault(s => s.SubjectId == id);
        }

        public int CreateSubject(Subject subject)
        {
            if (subject == null) return 0;
            try
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                return subject.SubjectId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }




    }
}
