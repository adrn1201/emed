using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMedFinalProject.Models
{
    public class OrderDetailsViewModel
    {
        public int OrderDetailID { get; set; }
        public virtual Order Order { get; set; }
        public int? OrderID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Milligrams { get; set; }
        public decimal EstimatedPrice { get; set; }
        public string Instructions { get; set; }
    }
}
