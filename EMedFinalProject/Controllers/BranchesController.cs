using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMedFinalProject.Data;
using EMedFinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace EMedFinalProject.Controllers
{
    [Authorize]
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = await _context.Branches.Include(b => b.Pharmacy).ToListAsync();
            if (id != null)
            {
                applicationDbContext = await _context.Branches.Include(b => b.Pharmacy).Where(b => b.Pharmacy.PharmacyID == (int)id).ToListAsync();
                
            }
            var model = new OrderViewModel();
            model.Branches = applicationDbContext;
            return View(model);


        }

        public IActionResult Create()
        {
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacies, "PharmacyID", "PharmacyID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranchLocation,ZipCode,DateAdded,PharmacyID")] Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    branch.DateAdded = DateTime.Now;
                    _context.Add(branch);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PharmacyID"] = new SelectList(_context.Pharmacies, "PharmacyID", "PharmacyID", branch.PharmacyID);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(branch);
            
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacies, "PharmacyID", "PharmacyID", branch.PharmacyID);
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BranchId,BranchLocation,ZipCode, DateModified,PharmacyID")] Branch branch)
        {
            if (id != branch.BranchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    branch.DateModified = DateTime.Now;
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.BranchId))
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
            ViewData["PharmacyID"] = new SelectList(_context.Pharmacies, "PharmacyID", "PharmacyID", branch.PharmacyID);
            return View(branch);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var branch = _context.Branches.Where(b => b.BranchId == id).SingleOrDefault();

            if (branch == null)
            {
                return RedirectToAction("Index");
            }


            _context.Branches.Remove(branch);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
      

        private bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.BranchId == id);
        }
    }
}
