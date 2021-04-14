﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Home(bool success = false)
        {
            ViewBag.success = success;
            return View();
        }

        public async Task<IActionResult> ConfirmedOrders()
        {
            return View(await _context.Orders.ToListAsync());
           
        }

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

