public class SlotOfWeekDTO
{
    public int Id { get; set; }
    public int SlotId { get; set; }
    public int DayOfWeekId { get; set; }

    public DayOfWeekDTO DayOfWeek { get; set; } = null!;
    public SlotGetDTO Slot { get; set; } = null!;
}

public class DayOfWeekDTO
{
    public int Id { get; set; }
    public string? DayOfWeek1 { get; set; } // Should be a string representation of the day, e.g., "Monday", "Tuesday"
}

public class SlotGetDTO
{
    // Properties for SlotGetDTO
}

public class SlotRepository
{
    private readonly Dictionary<int, string> _dayOfWeekMapping = new()
    {
        { 1, "Monday" },
        { 2, "Wednesday" }
    };

    public SlotOfWeekDTO GetSlotOfWeekById(int id)
    {
        // Implementation to get SlotOfWeekDTO by id
        // For example purpose, returning a mock object
        return new SlotOfWeekDTO
        {
            Id = id,
            SlotId = id,
            DayOfWeekId = id,
            DayOfWeek = new DayOfWeekDTO
            {
                Id = id,
                DayOfWeek1 = _dayOfWeekMapping.ContainsKey(id) ? _dayOfWeekMapping[id] : "Monday"
            },
            Slot = new SlotGetDTO()
        };
    }
}

public class DateSlotPair
{
    public DateTime Date { get; set; }
    public SlotOfWeekDTO Slot { get; set; }
}

public class Program
{
    public static void Main()
    {
        SlotRepository _slotRepository = new SlotRepository();
        List<int> slotIds = new List<int> { 1, 2 }; // Example slot IDs
        List<SlotOfWeekDTO> slots = new List<SlotOfWeekDTO>();

        foreach (var item in slotIds)
        {
            var slot = _slotRepository.GetSlotOfWeekById(item);
            slots.Add(slot);
        }

        DateTime startDate = new DateTime(2024, 9, 1);
        List<DateSlotPair> matchingDates = new List<DateSlotPair>();

        for (int week = 0; week < 10; week++)
        {
            foreach (var slot in slots)
            {
                if (Enum.TryParse(slot.DayOfWeek.DayOfWeek1, out DayOfWeek dayOfWeek))
                {
                    DateTime matchingDate = GetNextWeekday(startDate, dayOfWeek).AddDays(week * 7);
                    matchingDates.Add(new DateSlotPair { Date = matchingDate, Slot = slot });
                }
            }
        }

        foreach (var pair in matchingDates)
        {
            Console.WriteLine($"Date: {pair.Date:yyyy-MM-dd}, Slot ID: {pair.Slot.SlotId}, Day: {pair.Slot.DayOfWeek.DayOfWeek1}");
        }
    }

    public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }
}
