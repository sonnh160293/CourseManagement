using DTO.PostDTO;

namespace Student_Management_API.Helpers
{
    public class Helper
    {
        public Helper()
        {

        }

        public DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public List<GradeSubjecPostDTO> AddGrade(int subjectId)
        {
            List<GradeSubjecPostDTO> gradeSubjecPostDTOs = new List<GradeSubjecPostDTO>();
            for (int i = 1; i <= 3; i++)
            {
                var subjectGrade = new GradeSubjecPostDTO()
                {
                    SubjectId = subjectId,
                    GradeCategoryId = 1,
                    GradeItem = "Assignment " + i,
                    Weight = 5

                };

                gradeSubjecPostDTOs.Add(subjectGrade);
            }

            for (int i = 1; i <= 2; i++)
            {
                var subjectGrade = new GradeSubjecPostDTO()
                {
                    SubjectId = subjectId,
                    GradeCategoryId = 2,
                    GradeItem = "Progress Test " + i,
                    Weight = 10

                };

                gradeSubjecPostDTOs.Add(subjectGrade);
            }

            var subjectGradePRJ = new GradeSubjecPostDTO()
            {
                SubjectId = subjectId,
                GradeCategoryId = 3,
                GradeItem = "Group Project",
                Weight = 20
            };
            gradeSubjecPostDTOs.Add(subjectGradePRJ);

            var subjectGradeFe = new GradeSubjecPostDTO()
            {
                SubjectId = subjectId,
                GradeCategoryId = 5,
                GradeItem = "Final Exam",
                Weight = 25
            };
            gradeSubjecPostDTOs.Add(subjectGradeFe);

            var subjectGradeFeResit = new GradeSubjecPostDTO()
            {
                SubjectId = subjectId,
                GradeCategoryId = 6,
                GradeItem = "Final Exam Resit",
                Weight = 25
            };
            gradeSubjecPostDTOs.Add(subjectGradeFeResit);

            var subjectGradePE = new GradeSubjecPostDTO()
            {
                SubjectId = subjectId,
                GradeCategoryId = 4,
                GradeItem = "Pratical Exam",
                Weight = 20
            };
            gradeSubjecPostDTOs.Add(subjectGradePE);
            return gradeSubjecPostDTOs;

        }
    }
}
