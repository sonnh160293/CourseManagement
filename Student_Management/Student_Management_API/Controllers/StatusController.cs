using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ITypeRepository _typeRepository;
        public StatusController(IStatusRepository statusRepository, ITypeRepository typeRepository)
        {
            _statusRepository = statusRepository;
            _typeRepository = typeRepository;
        }


        [HttpGet("GetStatus")]
        public IActionResult GetStatus()
        {
            var statuses = _statusRepository.GetStatuses();
            if (statuses == null || statuses.Count == 0)
            {
                return NotFound();
            }
            return Ok(statuses);
        }

        [HttpGet("GetStatus/{typeId}")]
        public IActionResult GetStatus(int typeId)
        {
            var statuses = _statusRepository.GetStatusesByTypeId(typeId);
            if (statuses == null || statuses.Count == 0)
            {
                return NotFound();
            }
            return Ok(statuses);
        }

        [HttpGet("GetType")]
        public IActionResult GetTypes()
        {
            var types = _typeRepository.GetTypes();
            if (types == null || types.Count == 0)
            {
                return NotFound();
            }
            return Ok(types);
        }

    }
}
