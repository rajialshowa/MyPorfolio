using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Core.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AboutMesController : Controller
    {
        private readonly IUnitOfWork<AboutMe> _aboutMe;

        public AboutMesController(IUnitOfWork<AboutMe> aboutMe)
        {
            _aboutMe = aboutMe;
        }

        // GET: AboutMes
        public IActionResult Index()
        {
            return View(_aboutMe.Entity.GetAll());
        }

        // GET: AboutMes/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutMe = _aboutMe.Entity.GetById(id);
            if (aboutMe == null)
            {
                return NotFound();
            }

            return View(aboutMe);
        }

        // GET: AboutMes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutMes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AboutMe model)
        {
            if (ModelState.IsValid)
            {
                AboutMe aboutMe = new AboutMe
                {
                    Section1 = model.Section1,
                    Section2 = model.Section2
                };
                _aboutMe.Entity.Insert(aboutMe);
                _aboutMe.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: AboutMes/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutMe = _aboutMe.Entity.GetById(id);
            if (aboutMe == null)
            {
                return NotFound();
            }
            AboutMeViewModel aboutMeViewModel = new AboutMeViewModel
            {
                Id = aboutMe.Id,
                Section1 = aboutMe.Section1,
                Section2 = aboutMe.Section2
            };
            return View(aboutMeViewModel);
        }

        // POST: AboutMes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, AboutMeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AboutMe aboutMe = new AboutMe
                    {
                        Id = model.Id,
                        Section1 = model.Section1,
                        Section2 = model.Section2
                    };
                    _aboutMe.Entity.Update(aboutMe);
                    _aboutMe.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutMeExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: AboutMes/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutMe = _aboutMe.Entity.GetById(id);
            if (aboutMe == null)
            {
                return NotFound();
            }

            return View(aboutMe);
        }

        // POST: AboutMes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            
            _aboutMe.Entity.Delete(id);
            _aboutMe.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutMeExists(Guid id)
        {
            return _aboutMe.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
