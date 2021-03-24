using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMedFinalProject.Data;
using EMedFinalProject.Models;

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
        public IActionResult OrderProcess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderProcess(OrderViewModel record)
        {
            //StartWizard Step 1
            var order = new OrderViewModel();
            {
                order.LastName = record.LastName;
                order.FirstName = record.FirstName;
                order.DeliveryAddress = record.DeliveryAddress;
                order.ContactNo = record.ContactNo;
                order.Email = record.Email;
                //order.Prescription = record.Prescription;
                //order.ValidID = record.ValidID;
                order.BranchID = record.BranchID;//temporary placement
                order.PaymentMethod = record.PaymentMethod;//temporary placement
                   
            }

            /*Next...*/
            return View();
        }
    }
}
