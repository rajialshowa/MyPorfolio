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
    public class ContactMesController : Controller
    {
        private readonly IUnitOfWork<ContactMe> _contactMe;

        public ContactMesController(IUnitOfWork<ContactMe> contactMe)
        {
            _contactMe = contactMe;
        }

        // GET: ContactMes
        public IActionResult Index()
        {
            try
            {
                return View(_contactMe.Entity.GetAll());
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: ContactMes/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMe = _contactMe.Entity.GetById(id);
            if (contactMe == null)
            {
                return NotFound();
            }

            return View(contactMe);
        }

        // GET: ContactMes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactMes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactMeViewModel model)
        {
            if (ModelState.IsValid)
            {
                ContactMe contactMe = new ContactMe
                {
                    Fullname = model.Fullname,
                    Email = model.Email,
                    Phone = model.Phone,
                    Message = model.Message
                };
                _contactMe.Entity.Insert(contactMe);
                _contactMe.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ContactMes/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMe = _contactMe.Entity.GetById(id);
            if (contactMe == null)
            {
                return NotFound();
            }
            ContactMeViewModel contactMeViewModel = new ContactMeViewModel
            {
                Id = contactMe.Id,
                Fullname = contactMe.Fullname,
                Email = contactMe.Email,
                Phone = contactMe.Phone,
                Message = contactMe.Message
            };
            return View(contactMe);
        }

        // POST: ContactMes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ContactMeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContactMe contactMe = new ContactMe
                    {
                        Id = model.Id,
                        Fullname = model.Fullname,
                        Email = model.Email,
                        Phone = model.Phone,
                        Message = model.Message
                    };
                    _contactMe.Entity.Update(contactMe);
                    _contactMe.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactMeExists(model.Id))
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

        // GET: ContactMes/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMe = _contactMe.Entity.GetById(id);
            if (contactMe == null)
            {
                return NotFound();
            }

            return View(contactMe);
        }

        // POST: ContactMes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _contactMe.Entity.Delete(id);
            _contactMe.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactMeExists(Guid id)
        {
            return _contactMe.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
