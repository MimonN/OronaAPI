using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CleaningTypeDto
    {
        public int Id { get; set; }
        public string CleaningName { get; set; }

        public List<Product>? Products { get; set; }
    }
}
