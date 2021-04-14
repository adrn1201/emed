using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EMedFinalProject.Models
{
    public class OrdersDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public virtual Order Order { get; set; }
        public int? OrderID { get; set; }

        [Required(ErrorMessage ="Input a Product to Order")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity of Product is Required.")]
        [Range(1, 10, ErrorMessage = "Maximum of 10 products only.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Milligrams of Product is Required.")]
        public int Milligrams { get; set; }

        [Range(0.00, 2000.00, ErrorMessage = "Invalid price range.")]
        [Required]
        public decimal EstimatedPrice { get; set; }

        [DataType(DataType.MultilineText)]
        public string Instructions { get; set; }


    }
}
