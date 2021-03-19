using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EMedFinalProject.Models
{
    public class Pharmacy
    {
        [Key]
        public int PharmacyID { get; set; }
        public string Name { get; set; }

        public IList<Branch> Branches { get; set; }
    }
}
