using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CurriculumDAO
    {
        private StudentManagementContext _context;
        public CurriculumDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Curriculum> GetCurriculums(int? majorId)
        {
            List<Curriculum> curriculums = _context.Curricula.Include(c => c.Major).ToList();
            if (majorId != null && majorId > 0)
            {
                curriculums = curriculums.Where(c => c.MajorId == majorId).ToList();
            }
            return curriculums;
        }

        public Curriculum GetCurriculumById(int id)
        {
            return _context.Curricula.Include(c => c.Major).FirstOrDefault(c => c.CurriculumId == id);
        }

        public int AddCurriculum(Curriculum curriculum)
        {
            if (curriculum == null)
            {
                return 0;
            }
            try
            {
                _context.Curricula.Add(curriculum);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        public List<SubjectCurriculum> GetSubjectsInCurriculum(int curriculumId)
        {
            return _context.SubjectCurricula.Include(sc => sc.Subject)
                .Include(sc => sc.Curriculum)
                .Where(c => c.CurriculumId == curriculumId).ToList();
        }

        public int AddSubjectToCurriculum(SubjectCurriculum subjectCurriculum)
        {
            if (subjectCurriculum == null)
            {
                return 0;
            }
            try
            {
                _context.SubjectCurricula.Add(subjectCurriculum);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        public int AddSubjectsToCurriculum(List<SubjectCurriculum> subjectCurriculums)
        {
            if (subjectCurriculums == null)
            {
                return 0;
            }
            try
            {
                _context.SubjectCurricula.AddRange(subjectCurriculums);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public SubjectCurriculum GetSubjectInCurriculum(int subjectId, int curriculumId)
        {
            return _context.SubjectCurricula.Include(sc => sc.Subject).Include(sc => sc.Curriculum)
                .FirstOrDefault(sc => sc.SubjectId == subjectId && sc.CurriculumId == curriculumId);
        }

        public bool isSubjectExistInCurriculum(int subjectId, int curriculumId)
        {
            return GetSubjectInCurriculum(subjectId, curriculumId) == null ? false : true;
        }






    }
}
