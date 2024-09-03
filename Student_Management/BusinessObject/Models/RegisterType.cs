using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class RegisterType
    {
        public RegisterType()
        {
            Registrations = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public string? RegistrationName { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
