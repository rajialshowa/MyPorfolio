using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItems> _portfolio;
        [Obsolete]
        private readonly IHostingEnvironment _hosting;

        [Obsolete]
        public PortfolioItemsController(IUnitOfWork<PortfolioItems> portfolio, IHostingEnvironment hosting)
        {
            _portfolio = portfolio;
            _hosting = hosting;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItems = _portfolio.Entity.GetById(id);

            if (portfolioItems == null)
            {
                return NotFound();
            }

            return View(portfolioItems);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullpath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                    PortfolioItems portfolioItems = new PortfolioItems
                    {
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    _portfolio.Entity.Insert(portfolioItems);
                    _portfolio.Save();
                }
                else
                {
                    PortfolioItems portfolioItems = new PortfolioItems
                    {
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = "Avatar.jpg"
                    };
                    _portfolio.Entity.Insert(portfolioItems);
                    _portfolio.Save();
                }
               
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItems = _portfolio.Entity.GetById(id);

            if (portfolioItems == null)
            {
                return NotFound();
            }
            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                Id = portfolioItems.Id,
                Description = portfolioItems.Description,
                ImageUrl = portfolioItems.ImageUrl,
                ProjectName = portfolioItems.ProjectName
            };
            return View(portfolioViewModel);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel model)
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
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullpath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));

                        PortfolioItems portfolioItems = new PortfolioItems
                        {
                            Id = model.Id,
                            ProjectName = model.ProjectName,
                            Description = model.Description,
                            ImageUrl = model.File.FileName
                        };
                        _portfolio.Entity.Update(portfolioItems);
                        _portfolio.Save();
                    }
                    else
                    {
                        PortfolioItems portfolioItems = new PortfolioItems
                        {
                            Id = model.Id,
                            ProjectName = model.ProjectName,
                            Description = model.Description,
                            ImageUrl = model.ImageUrl
                        };
                        _portfolio.Entity.Update(portfolioItems);
                        _portfolio.Save();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemsExists(model.Id))
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


        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItems = _portfolio.Entity.GetById(id);
            if (portfolioItems == null)
            {
                return NotFound();
            }

            return View(portfolioItems);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemsExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
