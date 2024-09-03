using DTO.GetDTO;

namespace Repository.IRepository
{
    public interface ISLotRepository
    {
        public List<SlotGetDTO> GetSlots();
        public SlotGetDTO GetSlotById(int id);

        public List<SlotOfWeekDTO> GetSlotInDay(int dayId);

        public List<SlotOfWeekDTO> GetSlotInWeek();
        public SlotOfWeekDTO GetSlotOfWeekById(int id);
    }
}
