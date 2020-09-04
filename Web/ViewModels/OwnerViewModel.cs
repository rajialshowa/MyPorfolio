using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class OwnerViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string Profil { get; set; }
        public string Avatar { get; set; }
        public IFormFile File { get; set; }
        public Address Address { get; set; }
    }
}
