using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class AboutMeViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Left Section")]
        public string Section1 { get; set; }
        [DisplayName("Right Section")]
        public string Section2 { get; set; }
    }
}
