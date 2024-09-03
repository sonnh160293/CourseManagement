using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult GetStudents(int? majorId, string? studentCode, int? currentTerm)
        {
            var students = _studentRepository.GetStudents(majorId, studentCode, currentTerm);
            if (students == null || students.Count == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("id")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
