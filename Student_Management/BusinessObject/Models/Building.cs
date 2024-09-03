using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Building
    {
        public Building()
        {
            Rooms = new HashSet<Room>();
        }

        public int BuildingId { get; set; }
        public string? BuildingName { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
