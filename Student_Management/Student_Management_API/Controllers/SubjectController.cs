using DTO.Common;
using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public IActionResult GetSubjects(int? majord, int? term, int? adminId, string? subjectName, bool? status)
        {
            var subjects = _subjectRepository.GetSubjects(majord, term, adminId, subjectName, status);
            if (subjects.Count == 0)
            {
                return NotFound();
            }
            return Ok(subjects);
        }

        [HttpGet("GetSubjectsPrequisite")]
        public IActionResult GetSubjectsPrequisite(int majord, int term)
        {
            var subjects = _subjectRepository.GetSubjectsPrequisite(majord, term);
            if (subjects.Count == 0)
            {
                return NotFound();
            }
            return Ok(subjects);
        }

        [HttpPost("CreateSubject")]
        public IActionResult CreateSubject(SubjectPostDTO subjectPostDTO)
        {
            if (subjectPostDTO == null)
            {
                return BadRequest();
            }
            subjectPostDTO.CreatedBy = 1;
            try
            {

                int subjectId = _subjectRepository.AddSubject(subjectPostDTO);
                {
                    return StatusCode(StatusCodes.Status201Created, subjectId);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Create fail");
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(ErrorMessage.INVALID))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Term prequisite must < term subject");

                }

                else if (ex.Message.Equals(ErrorMessage.UNMATCH))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Subject prequiste and subject must be in the same major");

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error " + ex.Message);

                }
            }

        }

        [HttpGet("{subjectId}")]
        public IActionResult GetSubjectById(int subjectId)
        {
            var subject = _subjectRepository.GetSubjectById(subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
    }
}
