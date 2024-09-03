using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public IActionResult GetTeachers(int? majorId)
        {
            var teachers = _teacherRepository.GetTeachers(majorId);
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeachers(int id)
        {
            var teacher = _teacherRepository.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }
    }
}
