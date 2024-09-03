using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISLotRepository _sLotRepository;
        public SlotController(ISLotRepository sLotRepository)
        {
            _sLotRepository = sLotRepository;
        }

        [HttpGet]
        public IActionResult GetSlots()
        {
            var slots = _sLotRepository.GetSlots();
            if (slots.Count == 0)
            {
                return NotFound();
            }
            return Ok(slots);
        }

        [HttpGet("{id}")]
        public IActionResult GetSlotById(int id)
        {
            var slot = _sLotRepository.GetSlotById(id);
            if (slot == null)
            {
                return NotFound();
            }
            return Ok(slot);
        }

        [HttpGet("SlotInDay/{dayId}")]
        public IActionResult GetSLotInDay(int dayId)
        {
            var slotsInDay = _sLotRepository.GetSlotInDay(dayId);
            if (slotsInDay.Count == 0)
            {
                return NotFound();
            }
            return Ok(slotsInDay);
        }

        [HttpGet("SlotInWeek")]
        public IActionResult GetSLotInWeek()
        {
            var slotsInWeek = _sLotRepository.GetSlotInWeek();
            if (slotsInWeek.Count == 0)
            {
                return NotFound();
            }
            return Ok(slotsInWeek);
        }
    }
}
