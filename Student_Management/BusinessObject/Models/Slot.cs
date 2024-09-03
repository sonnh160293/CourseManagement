using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Slot
    {
        public Slot()
        {
            SlotOfWeeks = new HashSet<SlotOfWeek>();
        }

        public int SlotId { get; set; }
        public string? SlotName { get; set; }
        public string? FromHour { get; set; }
        public string? ToHour { get; set; }

        public virtual ICollection<SlotOfWeek> SlotOfWeeks { get; set; }
    }
}
