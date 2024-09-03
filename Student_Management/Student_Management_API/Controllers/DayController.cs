using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly IDayRepository _dayRepository;
        public DayController(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        [HttpGet]
        public IActionResult GetDays()
        {
            return Ok(_dayRepository.GetDayOfWeeks());
        }
    }
}
