using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ContactMeViewModel
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
