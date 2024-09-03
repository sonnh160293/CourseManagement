using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class SlotOfWeek
    {
        public SlotOfWeek()
        {
            CourseSchedules = new HashSet<CourseSchedule>();
        }

        public int Id { get; set; }
        public int SlotId { get; set; }
        public int DayOfWeekId { get; set; }

        public virtual DayOfWeek DayOfWeek { get; set; } = null!;
        public virtual Slot Slot { get; set; } = null!;
        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
    }
}
