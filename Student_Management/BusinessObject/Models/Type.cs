using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Type
    {
        public Type()
        {
            Statuses = new HashSet<Status>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<Status> Statuses { get; set; }
    }
}
