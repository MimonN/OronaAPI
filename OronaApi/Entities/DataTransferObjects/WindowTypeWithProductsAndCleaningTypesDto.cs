using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WindowTypeWithProductsAndCleaningTypesDto
    {
        public int Id { get; set; }
        public string WindowTypeName { get; set; }
        public string? ImageUrl { get; set; }
        public List<Product>? Products { get; set; }
        public List<CleaningType>? CleaningTypes { get; set; }
    }
}
