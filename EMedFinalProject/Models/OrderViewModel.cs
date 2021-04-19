using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMedFinalProject.Models
{
    public class OrderViewModel
    {
        //public int OrderID { get; set; }
        //public object Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Prescription { get; set; }
        public string ValidID { get; set; }

        //public virtual Branch Branch { get; set; }
        public int MethodID { get; set; }
        public Order Orders { get; set; }
        public List<Branch> Branches { get; set; }
        public int BranchID { get; set; }
        public OrdersDetail OrderDetails { get; set; }

        public List<OrdersDetail> Details { get; set; }
        public List<OrderDetailsViewModel> OrderList { get; set; }
        public PaymentMethod PaymentMethods { get; set; }

    }
}
