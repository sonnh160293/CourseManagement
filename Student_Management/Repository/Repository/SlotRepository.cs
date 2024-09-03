using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.GetDTO;
using Repository.IRepository;


namespace Repository.Repository
{
    public class SlotRepository : ISLotRepository
    {
        private readonly StudentManagementContext _context;
        private readonly IMapper _mapper;
        public SlotRepository(StudentManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SlotGetDTO GetSlotById(int id)
        {
            SlotDAO slotDAO = new SlotDAO(_context);
            return _mapper.Map<SlotGetDTO>(slotDAO.GetSlotById(id));
        }

        public List<SlotOfWeekDTO> GetSlotInDay(int dayId)
        {
            SlotDAO slotDAO = new SlotDAO(_context);
            return _mapper.Map<List<SlotOfWeekDTO>>(slotDAO.GetSlotInDay(dayId));

        }

        public List<SlotOfWeekDTO> GetSlotInWeek()
        {
            SlotDAO slotDAO = new SlotDAO(_context);
            return _mapper.Map<List<SlotOfWeekDTO>>(slotDAO.GetSlotInWeek());
        }

        public SlotOfWeekDTO GetSlotOfWeekById(int id)
        {
            SlotDAO slotDAO = new SlotDAO(_context);
            return _mapper.Map<SlotOfWeekDTO>(slotDAO.GetSlotOfWeekById(id));
        }

        public List<SlotGetDTO> GetSlots()
        {
            SlotDAO slotDAO = new SlotDAO(_context);
            return _mapper.Map<List<SlotGetDTO>>(slotDAO.GetSlots());
        }


    }
}
