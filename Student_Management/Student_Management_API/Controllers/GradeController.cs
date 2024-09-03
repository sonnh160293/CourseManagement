using DTO.PostDTO;
using DTO.UpdateDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using Student_Management_API.Helpers;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        [HttpGet("GradesOfSubject")]
        public IActionResult GetGradesOfSubject(int subjectId)
        {
            var gradesOfSubject = _gradeRepository.GetGradesOfSubject(subjectId);
            if (gradesOfSubject == null)
            {
                return NotFound();
            }
            return Ok(gradesOfSubject);
        }

        [HttpPost("UpdateGrades")]
        public IActionResult UpdateGradesOfSubject(List<GradeUpdateDTO> gradeUpdateDTOs)
        {
            if (gradeUpdateDTOs == null)
            {
                return BadRequest();
            }
            if (_gradeRepository.UpdateGradesOfSubject(gradeUpdateDTOs))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("AddGradeToSubject")]
        public IActionResult AddGradesToSubject(int subjectId)
        {
            Helper helpers = new Helper();
            List<GradeSubjecPostDTO> gradeSubjecPostDTOs = helpers.AddGrade(subjectId);
            if (gradeSubjecPostDTOs == null)
            {
                return BadRequest();
            }
            try
            {
                int record = _gradeRepository.AddGradesToSubject(gradeSubjecPostDTOs);
                return Ok(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }




        }
    }
}
