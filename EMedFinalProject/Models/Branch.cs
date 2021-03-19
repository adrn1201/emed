using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EMedFinalProject.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Branch Location is Required.")]
        public string BranchLocation { get; set; }

        [Required(ErrorMessage = "ZipCode is Required")]
        public int ZipCode { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        public int PharmacyID { get; set; }

        public Pharmacy Pharmacy { get; set; }
    }
}
