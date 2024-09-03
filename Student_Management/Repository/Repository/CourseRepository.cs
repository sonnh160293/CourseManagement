using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using DTO.PostDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class CourseRepository : ICourseRepository
    {

        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddCourse(CoursePostDTO coursePostDTO)
        {
            if (coursePostDTO == null)
            {
                return 0;
            }
            CourseDAO courseDAO = new CourseDAO(_context);
            try
            {
                return courseDAO.AddCourse(_mapper.Map<Course>(coursePostDTO));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public bool AddCourseSchedule(List<CourseSchedulePostDTO> courseSchedulePostDTOs)
        {
            if (courseSchedulePostDTOs == null)
            {
                return false;
            }
            CourseDAO courseDAO = new CourseDAO(_context);
            try
            {
                if (courseDAO.AddCourseSchedule(_mapper.Map<List<CourseSchedule>>(courseSchedulePostDTOs)) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public CourseGetDTO GetCourseById(int courseId)
        {
            CourseDAO courseDAO = new CourseDAO(_context);
            return _mapper.Map<CourseGetDTO>(courseDAO.GetCourseById(courseId));
        }

        public List<CourseGetDTO> GetCourses(int? teacherId, int? status, int? semesterId, int? majorId)
        {
            CourseDAO courseDAO = new CourseDAO(_context);
            return _mapper.Map<List<CourseGetDTO>>(courseDAO.GetCourses(teacherId, status, semesterId, majorId));
        }

        public List<SlotOfWeekDTO> GetFreeSlot(int teacherId, int roomId)
        {
            CourseDAO courseDAO = new CourseDAO(_context);
            return _mapper.Map<List<SlotOfWeekDTO>>(courseDAO.GetFreeSlot(teacherId, roomId));
        }

        public List<CourseScheduleGetDTO> GetScheduleOfCourse(int courseId)
        {
            CourseDAO courseDAO = new CourseDAO(_context);
            return _mapper.Map<List<CourseScheduleGetDTO>>(courseDAO.GetScheduleOfCourse(courseId));
        }

        public List<SlotOfWeekDTO> GetSlotOfTeacherInWeek(int teacherId)
        {
            CourseDAO courseDAO = new CourseDAO(_context);
            return _mapper.Map<List<SlotOfWeekDTO>>(courseDAO.GetSlotOfTeacherInWeek(teacherId));
        }

        public List<StudentCourseGetDTO> GetStudentInCourse(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
