
using DTO.Common;
using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculumController : ControllerBase
    {
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly ISubjectRepository _subjectRepository;
        public CurriculumController(ICurriculumRepository curriculumRepository, ISubjectRepository subjectRepository)
        {
            _curriculumRepository = curriculumRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public IActionResult GetCurriculums(int? majorId)
        {
            var curriculums = _curriculumRepository.GetCurriculums(majorId);
            if (curriculums.Count == 0)
            {
                return NotFound();
            }
            return Ok(curriculums);
        }

        [HttpGet("{id}")]
        public IActionResult GetCurriculumById(int id)
        {
            var curriculum = _curriculumRepository.GetCurriculumById(id);
            if (curriculum == null)
            {
                return NotFound();
            }
            return Ok(curriculum);
        }

        [HttpGet("SubjectInCurriculum/{curriculumId}")]
        public IActionResult GetSubjectsInCurriculum(int curriculumId)
        {
            var subjectsInCurriculum = _curriculumRepository.GetSubjectsInCurriculum(curriculumId);
            if (subjectsInCurriculum.Count == 0)
            {
                return NotFound();
            }
            return Ok(subjectsInCurriculum);
        }

        [HttpPost("AddCurriculum")]
        public IActionResult AddCurriculum(CurriculumPostDTO curriculumRequestDTO)
        {
            if (_curriculumRepository.AddCurrculum(curriculumRequestDTO))
            {
                return StatusCode(StatusCodes.Status201Created, "Create success");

            }
            return StatusCode(StatusCodes.Status400BadRequest, "Create fail");

        }

        [HttpPost("AddSubjectToCurriculum")]
        public IActionResult AddSubjectToCurriculum(SubjectCurriculumPostDTO subjectCurriculumRequestDTO)
        {
            try
            {
                if (_curriculumRepository.AddSubjectCurriculum(subjectCurriculumRequestDTO))
                {
                    return StatusCode(StatusCodes.Status201Created, "Create success");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Create fail");
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Input is null"); // Thông báo lỗi khi curriculum là null
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(ErrorMessage.DUPLICATE))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Subject is existed");
                    // Thông báo lỗi khi Subject đã tồn tại trong curriculum
                }
                else if (ex.Message.Equals(ErrorMessage.UNMATCH))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Subject and Curriculum has to be same major");
                    // Thông báo lỗi khi Major không khớp
                }
                else if (ex.Message.Equals(ErrorMessage.NOT_ACTIVE))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Subject is not available");
                    // Thông báo lỗi khi Major không khớp
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error " + ex.Message);
                }
            }
        }


        [HttpPost("AddListSubjectToCurr")]
        public IActionResult AddListSubjectToCurriculum(int curriculumId, List<int> subjectIds)
        {
            var curriculum = _curriculumRepository.GetCurriculumById(curriculumId);

            //check curriculum exist
            if (curriculum == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Curriculum not exist");
            }


            //get exist subject + status true
            var subjects = _subjectRepository.GetSubjects(curriculum.MajorId, 0, 0, "", true);
            var subjectsToAdd = subjects.Where(s => subjectIds.Contains(s.SubjectId) && s.Status == true).ToList();

            if (subjectsToAdd.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No subject available");
            }

            //clear list subject id to add available id
            subjectIds.Clear();
            foreach (var item in subjectsToAdd)
            {
                subjectIds.Add(item.SubjectId);
            }

            //check if subject exist in curriculum
            var subjectCurriculum = _curriculumRepository.GetSubjectsInCurriculum(curriculumId).ToList();
            var subjectCurriculumNotToAdd = subjectCurriculum.Where(sc => subjectIds.Contains((int)sc.SubjectId)).ToList();
            //remove exist subject
            foreach (var item in subjectCurriculumNotToAdd)
            {
                subjectIds.Remove((int)item.SubjectId);
            }

            if (subjectIds.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No subject available");
            }


            //create subject curriculum
            List<SubjectCurriculumPostDTO> subjectCurriculumPostDTOs = new List<SubjectCurriculumPostDTO>();
            foreach (var item in subjectIds)
            {
                var curriculumPostDTO = new SubjectCurriculumPostDTO()
                {
                    CurriculumId = curriculumId,
                    SubjectId = item,
                    Status = true
                };
                subjectCurriculumPostDTOs.Add(curriculumPostDTO);
            };


            if (_curriculumRepository.AddSubjecsCurriculum(subjectCurriculumPostDTOs))
            {
                return Ok("Create success");
            }
            return BadRequest("Create fail");
        }

        [HttpGet("GetAvailableSubjects")]
        public IActionResult GetAvailableSubjects(int curriculumId)
        {
            var curriculum = _curriculumRepository.GetCurriculumById(curriculumId);

            var subjectCurriculum = _curriculumRepository.GetSubjectsInCurriculum(curriculumId);
            if (subjectCurriculum == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Curriculum not exist");
            }

            List<int> subjectInCurriculumId = new List<int>();
            foreach (var item in subjectCurriculum)
            {
                subjectInCurriculumId.Add((int)item.SubjectId);
            }

            var subjects = _subjectRepository.GetSubjects(curriculum.MajorId, 0, 0, "", true);
            var subjectsAvailabe = subjects.Where(s => !subjectInCurriculumId.Contains(s.SubjectId)).ToList();
            if (subjectsAvailabe == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No subject available");
            }
            return Ok(subjectsAvailabe);
        }
    }
}
