using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ProductCreateDto
    {
        public string? Description { get; set; }
        public double Price { get; set; }
        public int CleaningTypeId { get; set; }
        public int WindowTypeId { get; set; }
    }
}
