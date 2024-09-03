using DTO.GetDTO;
using DTO.PostDTO;

namespace Repository.IRepository
{
    public interface ICourseRepository
    {
        public int AddCourse(CoursePostDTO coursePostDTO);
        public List<CourseGetDTO> GetCourses(int? teacherId, int? status, int? semesterId, int? majorId);
        public List<SlotOfWeekDTO> GetSlotOfTeacherInWeek(int teacherId);
        public CourseGetDTO GetCourseById(int courseId);

        public List<CourseScheduleGetDTO> GetScheduleOfCourse(int courseId);

        public List<SlotOfWeekDTO> GetFreeSlot(int teacherId, int roomId);

        public bool AddCourseSchedule(List<CourseSchedulePostDTO> courseSchedulePostDTOs);

        public List<StudentCourseGetDTO> GetStudentInCourse(int courseId);
    }
}
