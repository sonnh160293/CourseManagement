using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IMajorRepository _majorRepository;
        public MajorController(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        [HttpGet]
        public IActionResult GetMajors()
        {
            var majors = _majorRepository.GetMajors();
            if (majors.Count == 0)
            {
                return NotFound();
            }
            return Ok(majors);
        }
    }
}
