using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Registration
    {
        public int Id { get; set; }
        public string? RegistrationName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsOpen { get; set; }
        public int? SemesterId { get; set; }
        public int? RegisterTypeId { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Admin? CreatedByNavigation { get; set; }
        public virtual RegisterType? RegisterType { get; set; }
    }
}
