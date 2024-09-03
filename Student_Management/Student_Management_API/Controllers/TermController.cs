using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        private readonly ITermRepository _termRepository;
        public TermController(ITermRepository termRepository)
        {
            _termRepository = termRepository;
        }

        [HttpGet]
        public IActionResult GetTerms()
        {
            return Ok(_termRepository.GetTerms());
        }
    }
}
