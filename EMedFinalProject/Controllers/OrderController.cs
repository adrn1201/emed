using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMedFinalProject.Data;
using EMedFinalProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EMedFinalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MercuryTerms()
        {
 
            return View();
        }
        public IActionResult WatsonsTerms()
        {
            return View();
        }
        public IActionResult TgpTerms()
        {
            return View();
        }
        public async Task<IActionResult> MercuryOrderProcess(int? id)
        {
            var applicationDbContext = await _context.Branches.Include(b => b.Pharmacy).ToListAsync();
            if (id != null)
            {
                applicationDbContext = await _context.Branches.Include(b => b.Pharmacy).Where(b => b.Pharmacy.PharmacyID == (int)id).ToListAsync();

            }
            var model = new BranchesViewModel();
            model.Branches = applicationDbContext;
            return View(model);
            //var branchList = _context.Branches.Where(b => b.PharmacyID == 1);
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> MercuryOrderProcess(OrderViewModel record, IFormFile prescription, IFormFile validId)
        {
            if (ModelState.IsValid)
            {
                if (prescription != null)
                {
                    if (prescription.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(prescription.FileName);
                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);
                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/prescription/", newFileName);
                        record.Orders.Prescription = newFileName;

                        using var stream = new FileStream(filePath, FileMode.Create);
                        prescription.CopyTo(stream);
                    }
                }

                if (validId != null)
                {
                    if (validId.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(validId.FileName);
                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);
                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/validId/", newFileName);
                        record.Orders.ValidID = newFileName;

                        using var stream = new FileStream(filePath, FileMode.Create);
                        validId.CopyTo(stream);
                    }
                }

                var branches = await _context.Branches.Where(b => b.BranchId == record.Orders.BranchID).SingleOrDefaultAsync();
                var order = new Order();
                {
                    order.LastName = record.Orders.LastName;
                    order.FirstName = record.Orders.FirstName;
                    order.DeliveryAddress = record.Orders.DeliveryAddress;
                    order.ContactNo = record.Orders.ContactNo;
                    order.Email = record.Orders.Email;
                    order.Branch = branches;
                    order.BranchID = record.Orders.BranchID;
                    order.PaymentMethodID = record.PaymentMethods.PaymentMethodID;
                    order.Prescription = record.Orders.Prescription;
                    order.ValidID = record.Orders.ValidID;

                }


                foreach (var productOrders in record.OrderList)
                {
                    var products = new OrdersDetail()
                    {
                        ProductName = productOrders.ProductName,
                        Milligrams = productOrders.Milligrams,
                        Quantity = productOrders.Quantity,
                        Instructions = productOrders.Instructions

                    };
                }

                return RedirectToAction("OrderForm", "Account", new { success = "yes" });

            }
            return View();
        }
       
    }
}

