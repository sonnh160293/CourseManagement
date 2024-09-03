using DTO.GetDTO;
using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using Student_Management_API.Helpers;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISLotRepository _slotRepository;
        public CourseController(ICourseRepository courseRepository, ISLotRepository slotRepository)
        {
            _courseRepository = courseRepository;
            _slotRepository = slotRepository;
        }

        [HttpGet("GetCourses")]
        public IActionResult GetCourss(int? teacherId, int? status, int? semesterId, int? majorId)
        {
            var courses = _courseRepository.GetCourses(teacherId, status, semesterId, majorId);
            if (courses == null || courses.Count == 0)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [HttpGet("id")]
        public IActionResult GetCourseById(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost("AddCourse")]
        public IActionResult AddCourse(CoursePostDTO coursePostDTO)
        {
            if (coursePostDTO == null)
            {
                return BadRequest();
            }
            try
            {
                int courseId = _courseRepository.AddCourse(coursePostDTO);
                return Ok(courseId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }


        [HttpGet("SlotInWeekOfTeacher")]
        public IActionResult GetSlotInWeekOfTeacher(int teacherId)
        {
            var slots = _courseRepository.GetSlotOfTeacherInWeek(teacherId);
            if (slots == null || slots.Count == 0)
            {
                return NotFound();
            }
            return Ok(slots);
        }

        [HttpGet("ScheduleOfCourse")]
        public IActionResult GetScheduleOfCourse(int courseId)
        {
            var schedules = _courseRepository.GetScheduleOfCourse(courseId);
            if (schedules == null || schedules.Count == 0)
            {
                return NotFound();
            }
            return Ok(schedules);
        }


        [HttpGet("FreeSlot")]
        public IActionResult GetFreeSlot(int teacherId, int roomId)
        {
            var slots = _courseRepository.GetFreeSlot(teacherId, roomId);
            if (slots == null || slots.Count == 0)
            {
                return NotFound();
            }
            return Ok(slots);
        }

        //[HttpGet("GetAvailableStudent")]
        //public IActionResult GetAvailableStudent(int courseId)
        //{
        //    var course = _courseRepository.GetCourseById(courseId);

        //    //check student major
        //    //check student term
        //    //checks student subjectPrequiste
        //    //check student schedule
        //}

        [HttpPost("AddCourseSchedule")]
        public IActionResult AddCourseSchedule(int courseId, int roomId, List<int> slotId)
        {
            var course = _courseRepository.GetCourseById(courseId);
            List<SlotOfWeekDTO> slots = new List<SlotOfWeekDTO>();
            foreach (var item in slotId)
            {
                var slot = _slotRepository.GetSlotOfWeekById(item);
                slots.Add(slot);
            }

            List<CourseSchedulePostDTO> courseSchedulePostDTOs = new List<CourseSchedulePostDTO>();
            DateTime startDate = new DateTime(2024, 9, 1);
            List<DateSlotPair> matchingDates = new List<DateSlotPair>();

            Helper helper = new Helper();
            for (int week = 0; week < 10; week++)
            {
                foreach (var slot in slots)
                {
                    if (Enum.TryParse(slot.DayOfWeek.DayOfWeek1, out DayOfWeek dayOfWeek))
                    {


                        DateTime matchingDate = helper.GetNextWeekday(startDate, dayOfWeek).AddDays(week * 7);
                        matchingDates.Add(new DateSlotPair { Date = matchingDate, Slot = slot });
                    }
                }
            }

            foreach (var pair in matchingDates)
            {
                var courseSchedule = new CourseSchedulePostDTO()
                {
                    CourseId = courseId,
                    Date = pair.Date,
                    Description = "",
                    RoomId = roomId,
                    SlotId = pair.Slot.Id
                };
                courseSchedulePostDTOs.Add(courseSchedule);
            }

            if (courseSchedulePostDTOs == null)
            {
                return BadRequest();
            }
            try
            {
                if (_courseRepository.AddCourseSchedule(courseSchedulePostDTOs))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
