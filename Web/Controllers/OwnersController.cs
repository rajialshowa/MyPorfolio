using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        [Obsolete]
        private readonly IHostingEnvironment _hosting;

        [Obsolete]
        public OwnersController(IUnitOfWork<Owner> owner, IHostingEnvironment hosting)
        {
            _owner = owner;
            _hosting = hosting;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_owner.Entity.GetAll());
        }

        // GET: Owners/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = _owner.Entity.GetById(id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullpath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                    Owner owner = new Owner
                    {
                        FullName = model.FullName,
                        Profil = model.Profil,
                        Avatar = model.File.FileName
                    };
                    _owner.Entity.Insert(owner);
                    _owner.Save();
                }
                else
                {
                    Owner owner = new Owner
                    {
                        FullName = model.FullName,
                        Profil = model.Profil,
                        Avatar = "Avatar.jpg"
                    };
                    _owner.Entity.Insert(owner);
                    _owner.Save();
                }

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        // GET: Owners/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = _owner.Entity.GetById(id);
            if (owner == null)
            {
                return NotFound();
            }
            OwnerViewModel ownerViewModel = new OwnerViewModel
            {
                Id = owner.Id,
                FullName = owner.FullName,
                Avatar = owner.Avatar,
                Profil= owner.Profil
                
            };
            return View(ownerViewModel);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Edit(Guid id, OwnerViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img");
                        string fullpath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                            Owner owner = new Owner
                            {
                                Id = model.Id,
                                FullName = model.FullName,
                                Profil = model.Profil,
                                Avatar = model.File.FileName
                            };
                        _owner.Entity.Update(owner);
                        _owner.Save();
                    }
                    else
                    {
                        Owner owner = new Owner
                        {
                            Id = model.Id,
                            FullName = model.FullName,
                            Profil = model.Profil,
                            Avatar = model.Avatar
                        };
                        _owner.Entity.Update(owner);
                        _owner.Save();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(model.Id))
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

        // GET: Owners/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner =_owner.Entity.GetById(id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {

            _owner.Entity.Delete(id);
            _owner.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(Guid id)
        {
            return _owner.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
