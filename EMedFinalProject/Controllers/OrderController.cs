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
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;

namespace EMedFinalProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Home(bool success = false)
        {
            ViewBag.success = success;
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact record)
        {
            using (MailMessage mail = new MailMessage("emed.webdevt.team@gmail.com", record.Email))
            {
                mail.Subject = record.Subject;

                string message = "Hello, " + record.SenderName + "<br/><br/>" +
                    "We have received your inquiry. Here are the details: <br/><br/>" +
                    "Contact Number: <strong>" + record.ContactNo + "</strong><br/>" +
                    "Message:<br/><strong>" + record.Message + "</strong><br/><br/>" +
                    "Please wait for our reply. Thank you!";

                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred =
                        new NetworkCredential("emed.webdevt.team@gmail.com", "e-MedPassword");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Inquiry sent";
                }
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ConfirmedOrders()
        {
            return View(await _context.Orders.ToListAsync());

        }
        [Authorize]
        public IActionResult ModifyStatus(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var order = _context.Orders.Where(i => i.OrderID == id).SingleOrDefault();
            if (order == null)
            {
                return RedirectToAction("ConfirmedOrders");
            }
            return View(order);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ModifyStatus(int? id, Order record)
        {
            var order = _context.Orders.Where(i => i.OrderID == id).SingleOrDefault();
            order.DateModified = DateTime.Now;
            order.Status = record.Status;

            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToAction("ConfirmedOrders");

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
            var model = new OrderViewModel();
            model.Branches = applicationDbContext;
            return View(model);
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

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/mercuryPrescriptions/", newFileName);
                        record.Prescription = newFileName;

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

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/mercuryvalidId/", newFileName);
                        record.ValidID = newFileName;

                        using var stream = new FileStream(filePath, FileMode.Create);
                        validId.CopyTo(stream);
                    }
                }

                var branch = await _context.Branches.Where(b => b.BranchId == record.BranchID).SingleOrDefaultAsync();
                var order = new Order();
                {
                    order.LastName = record.LastName;
                    order.FirstName = record.FirstName;
                    order.DeliveryAddress = record.DeliveryAddress;
                    order.ContactNo = record.ContactNo;
                    order.Email = record.Email;
                    order.Branch = branch;
                    order.DateAdded = DateTime.Now;
                    order.PaymentMethodID = record.MethodID;
                    order.Prescription = record.Prescription;
                    order.ValidID = record.ValidID;

                }

                _context.Orders.Add(order);



                foreach (var productOrders in record.OrderList)
                {
                    var products = new OrdersDetail()
                    {
                        Order = order,
                        ProductName = productOrders.ProductName,
                        Milligrams = productOrders.Milligrams,
                        Quantity = productOrders.Quantity,
                        EstimatedPrice = productOrders.EstimatedPrice,
                        Instructions = productOrders.Instructions

                    };
                    _context.OrderDetails.Add(products);
                }
                _context.SaveChanges();

                //using (MailMessage mail = new MailMessage("emed.webdevt.team@gmail.com", record.Email))
                //{
                //    mail.Subject = "Order Placement Status";

                //    string message = "Hello, " + record.LastName +", "+ record.FirstName + "<br/><br/>" +
                //        "Your Order Number is:<strong>"+record.Orders.OrderID +"</strong><br/><br/>" +
                //        "Ordered Products: <strong>" + record.OrderList + "</strong><br/>" +
                //        "Your order has successfully been placed. Thank you!";

                //    mail.Body = message;
                //    mail.IsBodyHtml = true;

                //    using (SmtpClient smtp = new SmtpClient())
                //    {
                //        smtp.Host = "smtp.gmail.com";
                //        smtp.EnableSsl = true;
                //        NetworkCredential NetworkCred =
                //            new NetworkCredential("emed.webdevt.team@gmail.com", "e-MedPassword");
                //        smtp.UseDefaultCredentials = false;
                //        smtp.Credentials = NetworkCred;
                //        smtp.Port = 587;
                //        smtp.Send(mail);
                //        ViewBag.Message = "Inquiry sent";
                //    }
                //}


                return RedirectToAction("Home", new { success = true });
                //return RedirectToAction("OrderForm", "Account", new { success = "yes" });

            }
            return View();
        }

        public async Task<IActionResult> WatsonsOrderProcess(int? id)
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

        [HttpPost]
        public async Task<IActionResult> WatsonsOrderProcess(OrderViewModel record, IFormFile prescription, IFormFile validId)
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

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/watsonsPrescription/", newFileName);
                        record.Prescription = newFileName;

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

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/watsonsValidId/", newFileName);
                        record.ValidID = newFileName;

                        using var stream = new FileStream(filePath, FileMode.Create);
                        validId.CopyTo(stream);
                    }
                }

                var branch = await _context.Branches.Where(b => b.BranchId == record.BranchID).SingleOrDefaultAsync();
                var order = new Order();
                {
                    order.LastName = record.LastName;
                    order.FirstName = record.FirstName;
                    order.DeliveryAddress = record.DeliveryAddress;
                    order.ContactNo = record.ContactNo;
                    order.Email = record.Email;
                    order.Branch = branch;
                    order.DateAdded = DateTime.Now;
                    order.PaymentMethodID = record.MethodID;
                    order.Prescription = record.Prescription;
                    order.ValidID = record.ValidID;

                }

                _context.Orders.Add(order);



                foreach (var productOrders in record.OrderList)
                {
                    var products = new OrdersDetail()
                    {
                        Order = order,
                        ProductName = productOrders.ProductName,
                        Milligrams = productOrders.Milligrams,
                        Quantity = productOrders.Quantity,
                        EstimatedPrice = productOrders.EstimatedPrice,
                        Instructions = productOrders.Instructions

                    };
                    _context.OrderDetails.Add(products);
                }
                _context.SaveChanges();


                return RedirectToAction("Home", new { success = true });
                //return RedirectToAction("OrderForm", "Account", new { success = "yes" });

            }
            return View();
        }

        public async Task<IActionResult> TgpOrderProcess(int? id)
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

        [HttpPost]
        public async Task<IActionResult> TgpOrderProcess(OrderViewModel record, IFormFile prescription, IFormFile validId)
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

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/tgpPrescription/", newFileName);
                        record.Prescription = newFileName;

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

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/TgpValidId/", newFileName);
                        record.ValidID = newFileName;

                        using var stream = new FileStream(filePath, FileMode.Create);
                        validId.CopyTo(stream);
                    }
                }

                var branch = await _context.Branches.Where(b => b.BranchId == record.BranchID).SingleOrDefaultAsync();
                var order = new Order();
                {
                    order.LastName = record.LastName;
                    order.FirstName = record.FirstName;
                    order.DeliveryAddress = record.DeliveryAddress;
                    order.ContactNo = record.ContactNo;
                    order.Email = record.Email;
                    order.Branch = branch;
                    order.DateAdded = DateTime.Now;
                    order.PaymentMethodID = record.MethodID;
                    order.Prescription = record.Prescription;
                    order.ValidID = record.ValidID;

                }

                _context.Orders.Add(order);



                foreach (var productOrders in record.OrderList)
                {
                    var products = new OrdersDetail()
                    {
                        Order = order,
                        ProductName = productOrders.ProductName,
                        Milligrams = productOrders.Milligrams,
                        Quantity = productOrders.Quantity,
                        EstimatedPrice = productOrders.EstimatedPrice,
                        Instructions = productOrders.Instructions

                    };
                    _context.OrderDetails.Add(products);
                }
                _context.SaveChanges();


                return RedirectToAction("Home", new { success = true });
                //return RedirectToAction("OrderForm", "Account", new { success = "yes" });

            }
            return View();
        }

    }
}

