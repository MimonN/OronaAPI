using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class WindowType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string WindowTypeName { get; set; }
        public string? ImageUrl { get; set; }
        public List<Product>? Products { get; set; }
    }
}
