using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;
        public SemesterController(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        [HttpGet]
        public IActionResult GetSemester(int? year)
        {
            var semester = _semesterRepository.GetSemester(year);
            if (semester == null || semester.Count == 0)
            {
                return NotFound();
            }
            return Ok(semester);
        }

        [HttpPost("AddSemester")]
        public IActionResult AddSemester(SemesterPostDTO semesterPostDTO)
        {
            if (semesterPostDTO == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Semester not null");
            }
            try
            {
                if (_semesterRepository.AddSemester(semesterPostDTO))
                {
                    return StatusCode(StatusCodes.Status201Created, "Create success");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Create failt");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error" + ex);
            }

        }
    }
}
