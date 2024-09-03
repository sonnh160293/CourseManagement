using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CourseDAO
    {
        private StudentManagementContext _context;
        public CourseDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public int AddCourse(Course course)
        {
            if (course == null)
            {
                return 0;
            }
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course.CourseId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;

            }
        }


        public List<Course> GetCourses(int? teacherId, int? status, int? semesterId, int? majorId)
        {
            var courses = _context.Courses.Include(c => c.StudentCourses)
                .Include(c => c.Subject).ThenInclude(c => c.Major)
                .Include(c => c.Teacher)
                .Include(c => c.Semester)
                .ToList();
            if (teacherId != null && teacherId > 0)
            {
                courses = courses.Where(c => c.TeacherId == teacherId).ToList();
            }
            if (status != null && status > 0)
            {
                courses = courses.Where(c => c.Status == status).ToList();

            }
            if (semesterId != null && semesterId > 0)
            {
                courses = courses.Where(c => c.SemesterId == semesterId).ToList();
            }
            if (majorId != null && majorId > 0)
            {
                courses = courses.Where(c => c.Subject.MajorId == majorId).ToList();
            }
            return courses;
        }


        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Include(c => c.StudentCourses)
                .Include(c => c.Subject).ThenInclude(c => c.Major)
                .Include(c => c.Teacher)
                .Include(c => c.Semester)
                .FirstOrDefault(c => c.CourseId == courseId);
        }


        public List<StudentCourse> GetStudentInCourse(int courseId)
        {
            return _context.StudentCourses.Include(sc => sc.Course).Include(sc => sc.Student)
                .Where(sc => sc.CourseId == courseId).ToList();
        }

        public int AddStudentsToCourse(List<StudentCourse> studentCourses)
        {
            if (studentCourses == null)
            {
                return 0;
            }
            try
            {
                _context.StudentCourses.AddRange(studentCourses);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<CourseSchedule> GetScheduleOfCourse(int courseId)
        {
            return _context.CourseSchedules.Include(cs => cs.Room).ThenInclude(cs => cs.Building)
                .Include(cs => cs.SlotOfWeek).ThenInclude(cs => cs.Slot)
                .Include(cs => cs.SlotOfWeek).ThenInclude(cs => cs.DayOfWeek)
                .Include(cs => cs.Course)
                .Where(cs => cs.CourseId == courseId).OrderBy(cs => cs.Date)
                .ToList();
        }

        public List<Course> GetCourseByTeacher(int teacherId)
        {
            return _context.Courses.Include(c => c.StudentCourses)
                .Include(c => c.Subject).ThenInclude(c => c.Major)
                .Include(c => c.Teacher)
                .Include(c => c.Semester)
                .Where(c => c.TeacherId == teacherId)
                .ToList();
        }

        public List<StudentCourse> GetCourseByStudent(int studentId)
        {
            return _context.StudentCourses.Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .Include(sc => sc.StudentCourseAttendances)
                .Where(sc => sc.StudentId == studentId)
                .ToList();
        }

        public List<CourseSchedule> GetScheduleOfStudent(int studentId)
        {
            List<CourseSchedule> schedules = new List<CourseSchedule>();
            var courses = GetCourseByStudent(studentId);

            foreach (var course in courses)
            {
                var scheduleOfCourse = GetScheduleOfCourse((int)course.CourseId);
                schedules.AddRange(scheduleOfCourse);
            }
            return schedules;


        }


        public List<CourseSchedule> GetScheduleOfTeacherInWeek(int teacherId)
        {
            List<CourseSchedule> courseSchedules = new List<CourseSchedule>();
            var courses = GetCourseByTeacher(teacherId);
            foreach (var course in courses)
            {
                var courseSchedule = GetScheduleOfCourse((int)course.CourseId);
                var distinctSchedule = courseSchedule.GroupBy(cs => cs.SlotId)
                                        .Select(g => g.First())
                                        .ToList();

                courseSchedules.AddRange(distinctSchedule);
            }
            return courseSchedules;

        }

        public List<SlotOfWeek> GetSlotOfTeacherInWeek(int teacherId)
        {
            List<SlotOfWeek> slotOfWeeks = new List<SlotOfWeek>();
            var courseSchedules = GetScheduleOfTeacherInWeek(teacherId);
            foreach (var course in courseSchedules)
            {
                var slot = _context.SlotOfWeeks.Include(s => s.DayOfWeek).Include(s => s.Slot).FirstOrDefault(s => s.Id == course.SlotId);
                slotOfWeeks.Add(slot);
            }
            return slotOfWeeks;

        }

        public List<CourseSchedule> GetScheduleOfRoomInWeek(int roomId)
        {

            return _context.CourseSchedules.Where(s => s.RoomId == roomId).GroupBy(cs => cs.SlotId).Select(g => g.First()).ToList();


        }

        public List<SlotOfWeek> GetSlotOfRoomInWeek(int roomId)
        {
            List<SlotOfWeek> slotOfWeeks = new List<SlotOfWeek>();
            var courseSchedules = GetScheduleOfRoomInWeek(roomId);
            foreach (var course in courseSchedules)
            {
                var slot = _context.SlotOfWeeks.Include(s => s.DayOfWeek).Include(s => s.Slot).FirstOrDefault(s => s.Id == course.SlotId);
                slotOfWeeks.Add(slot);
            }
            return slotOfWeeks;

        }

        public List<SlotOfWeek> GetFreeSlot(int teacherId, int roomId)
        {
            var roomSlot = GetSlotOfRoomInWeek(roomId);
            var teacherSlot = GetSlotOfTeacherInWeek(teacherId);
            var allSlot = _context.SlotOfWeeks.Include(s => s.Slot).Include(s => s.DayOfWeek).ToList();

            // Extract SlotIds for comparison
            var roomSlotIds = roomSlot.Select(rs => rs.Id).ToHashSet();
            var teacherSlotIds = teacherSlot.Select(ts => ts.Id).ToHashSet();

            // Find free slots by excluding slots in roomSlot and teacherSlot
            var freeSlot = allSlot.Where(s => !roomSlotIds.Contains(s.Id) && !teacherSlotIds.Contains(s.Id)).ToList();

            return freeSlot;
        }


        public int AddCourseSchedule(List<CourseSchedule> courseSchedules)
        {

            if (courseSchedules == null)
            {
                return 0;
            }
            try
            {
                _context.CourseSchedules.AddRange(courseSchedules);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }



        //public List<SlotOfWeek> GetSlotInWeekOfStudent(int studentId)
        //{
        //    List<SlotOfWeek> slotOfWeeks = new List<SlotOfWeek>();
        //    var schedules = GetScheduleOfStudent(studentId);
        //    foreach (var schedule in schedules)
        //    {
        //        var slotId = schedule.SlotId;
        //    }
        //}

    }
}
