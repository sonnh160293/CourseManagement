using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        public IActionResult GetAdmins()
        {
            var admins = _adminRepository.GetAdmins();
            if (admins.Count == 0)
            {
                return NotFound();
            }
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admin = _adminRepository.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        public IActionResult AddAdmin(AdminPostDTO adminRequestDTO)
        {
            if (_adminRepository.AddAdmin(adminRequestDTO))
            {
                return Ok("Create success");
            }
            return BadRequest("Create fail");
        }
    }
}
