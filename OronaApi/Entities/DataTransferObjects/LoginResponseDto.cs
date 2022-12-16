using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
