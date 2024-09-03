using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Room
    {
        public Room()
        {
            CourseSchedules = new HashSet<CourseSchedule>();
        }

        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? BuildingId { get; set; }

        public virtual Building? Building { get; set; }
        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
    }
}
