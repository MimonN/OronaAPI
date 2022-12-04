using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Models
{
    public class CleaningType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cleaning Type")]
        public string CleaningName { get; set; }

        public List<Product>? Products { get; set; }
    }
}
