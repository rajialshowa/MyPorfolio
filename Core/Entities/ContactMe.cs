using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ContactMe : EntityBase
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
