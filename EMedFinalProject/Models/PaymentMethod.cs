using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMedFinalProject.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodID { get; set; }
        public string Method { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
