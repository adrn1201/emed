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

        public IActionResult MercuryOrderProcess()
        {
            return View();
        }

        public IActionResult OrderProcess()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderProcess(OrderViewModel record) //IFormFile file)
        {
            //if (ModelState.IsValid)
            //{
            //    if (file != null)
            //    {
            //        if (file.Length > 0)
            //        {
            //            //Getting FileName
            //            var fileName = Path.GetFileName(file.FileName);
            //            //Getting file Extension
            //            var fileExtension = Path.GetExtension(fileName);
            //            // concatenating  FileName + FileExtension
            //            var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

            //            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services", newFileName);
            //            record.Prescription = newFileName;

            //            using var stream = new FileStream(filePath, FileMode.Create);
            //            file.CopyTo(stream);
            //        }
            //    }
                var branches = await _context.Branches.Where(b => b.BranchId == record.BranchID).SingleOrDefaultAsync();

                var order = new OrderViewModel();
                {
                    order.LastName = record.LastName;
                    order.FirstName = record.FirstName;
                    order.DeliveryAddress = record.DeliveryAddress;
                    order.ContactNo = record.ContactNo;
                    order.Email = record.Email;
                    order.Branch = branches;
                    order.BranchID = record.BranchID;
                    order.PaymentMethod = record.PaymentMethod;

                }

                //Dynamic Fields
                foreach (var productOrders in record.OrderList)
                {
                    var products = new OrderDetailsViewModel()
                    {
                        ProductName = productOrders.ProductName,
                        Milligrams = productOrders.Milligrams,
                        Quantity = productOrders.Quantity,
                        Instructions = productOrders.Instructions

                    };
                }

                return RedirectToAction("OrderForm", "Account", new { success = "yes" });
            }
        }
    }

