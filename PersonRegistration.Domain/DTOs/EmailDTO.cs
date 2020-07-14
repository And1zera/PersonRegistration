using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.DTOs
{
    public class EmailDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
