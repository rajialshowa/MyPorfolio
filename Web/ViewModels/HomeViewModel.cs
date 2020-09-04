using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public Owner    owner{ get; set; }
        public List<PortfolioItems> portfolioItems { get; set; }
        public AboutMe aboutMe { get; set; }
        public ContactMe ContactMe { get; set; }
    }
}
