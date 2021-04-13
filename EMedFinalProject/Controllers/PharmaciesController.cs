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
    public class PharmaciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmaciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pharmacies
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Pharmacies.ToListAsync());
        }

      
    }
}
