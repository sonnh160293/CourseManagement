using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class GradeDAO
    {
        private readonly StudentManagementContext _studentManagementContext;
        public GradeDAO(StudentManagementContext studentManagementContext)
        {
            _studentManagementContext = studentManagementContext;
        }

        public List<SubjectGrade> GetGradesOfSubject(int subjectId)
        {
            return _studentManagementContext.SubjectGrades.Include(sg => sg.GradeCategory)
                .Where(sg => sg.SubjectId == subjectId).OrderBy(sg => sg.GradeCategoryId).ToList();
        }

        public int AddGradeToSubject(SubjectGrade subjectGrade)
        {
            if (subjectGrade == null)
            {
                return 0;
            }
            _studentManagementContext.SubjectGrades.Add(subjectGrade);
            return _studentManagementContext.SaveChanges();
        }

        public int AddGradesToSubject(List<SubjectGrade> subjectGrades)
        {
            if (subjectGrades == null)
            {
                return 0;
            }
            _studentManagementContext.SubjectGrades.AddRange(subjectGrades);
            return _studentManagementContext.SaveChanges();
        }


        public int UpdateGradesOfSubject(List<SubjectGrade> subjectGrades)
        {
            if (subjectGrades == null || subjectGrades.Count == 0)
            {
                return 0;
            }

            int recordChange = 0;
            try
            {
                foreach (var subjectGrade in subjectGrades)
                {
                    var existingSubjectGrade = _studentManagementContext.SubjectGrades.Find(subjectGrade.Id);
                    if (existingSubjectGrade != null)
                    {
                        existingSubjectGrade.GradeItem = subjectGrade.GradeItem; // Cập nhật thuộc tính GradeItem
                        existingSubjectGrade.Weight = subjectGrade.Weight; // Cập nhật thuộc tính Weight

                        recordChange += _studentManagementContext.SaveChanges(); // Lưu thay đổi xuống cơ sở dữ liệu
                    }
                }

                return recordChange; // Trả về số lượng bản ghi đã được cập nhật
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }


    }
}
