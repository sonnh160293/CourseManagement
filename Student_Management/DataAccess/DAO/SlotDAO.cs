using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class SlotDAO
    {
        private readonly StudentManagementContext _context;

        public SlotDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Slot> GetSlots()
        {
            return _context.Slots.ToList();
        }

        public Slot GetSlotById(int id)
        {
            return _context.Slots.FirstOrDefault(s => s.SlotId == id);
        }

        public List<SlotOfWeek> GetSlotInDay(int dayId)
        {
            return _context.SlotOfWeeks.Include(s => s.DayOfWeek).Include(s => s.Slot).Where(s => s.DayOfWeekId == dayId).ToList();
        }

        public List<SlotOfWeek> GetSlotInWeek()
        {
            return _context.SlotOfWeeks.Include(s => s.DayOfWeek).Include(s => s.Slot).ToList();
        }

        public SlotOfWeek GetSlotOfWeekById(int id)
        {
            return _context.SlotOfWeeks.Include(s => s.DayOfWeek).Include(s => s.Slot).FirstOrDefault(s => s.Id == id);
        }
    }
}
