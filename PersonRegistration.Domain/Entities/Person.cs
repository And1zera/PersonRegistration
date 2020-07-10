using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Status { get; set; }
        public string Password { get; set; }
    }
}
