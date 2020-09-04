using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItems> _portfolio;
        private readonly IUnitOfWork<AboutMe> _aboutme;
        private readonly IUnitOfWork<ContactMe> _contactMe;

        public HomeController(IUnitOfWork<Owner> owner,
            IUnitOfWork<PortfolioItems> portfolio,
            IUnitOfWork<AboutMe> aboutme,
            IUnitOfWork<ContactMe> contactMe)
        {
            _owner = owner;
            _portfolio = portfolio;
            _aboutme = aboutme;
            _contactMe = contactMe;
        }
        public IActionResult Index()
        {
            var HomeViewModel = new HomeViewModel
            {
                owner = _owner.Entity.GetAll().First(),
                portfolioItems = _portfolio.Entity.GetAll().ToList(),
                aboutMe = _aboutme.Entity.GetAll().First()
            };
            return View(HomeViewModel);
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactMes()
        {
            //var contactMe = new ContactMe
            //{
            //    Fullname = contactMe.Fullname,
            //    Email = contactMe.Email,
            //    Phone = contactMe.Phone,
            //    Message = contactMe.Message
            //};
            //model.Fullname = HttpContext.Request.Form["Fullname"].ToString();
            //model.Email = HttpContext.Request.Form["Email"].ToString();
            ////model.Email = Convert.ToInt32(HttpContext.Request.Form["txtAge"]);
            //model.Phone = HttpContext.Request.Form["Phone"].ToString();
            //model.Message = HttpContext.Request.Form["Message"].ToString();
            ////int result = model.Sa();
            ////if (result > 0)
            ////{
            ////    ViewBag.Result = "Data Saved Successfully";
            ////}
            ////else
            ////{
            ////    ViewBag.Result = "Something Went Wrong";
            ////}
            //_contactMe.Entity.Insert(contactMe);
            //_contactMe.Save();
            return View();
        }
    public IActionResult About()
        {
            return View();
        }
    }
}