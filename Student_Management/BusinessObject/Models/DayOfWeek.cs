using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class DayOfWeek
    {
        public DayOfWeek()
        {
            SlotOfWeeks = new HashSet<SlotOfWeek>();
        }

        public int Id { get; set; }
        public string? DayOfWeek1 { get; set; }

        public virtual ICollection<SlotOfWeek> SlotOfWeeks { get; set; }
    }
}
