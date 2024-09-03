using DayOfWeek = BusinessObject.Models.DayOfWeek;
namespace Repository.IRepository
{
    public interface IDayRepository
    {
        public List<DayOfWeek> GetDayOfWeeks();
    }
}
