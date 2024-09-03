using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;
        public BuildingController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }


        [HttpGet]
        public IActionResult GetBuilding()
        {
            var buildings = _buildingRepository.GetBuildings();
            if (buildings.Count == 0)
            {
                return NotFound();
            }
            return Ok(buildings);
        }
    }
}
