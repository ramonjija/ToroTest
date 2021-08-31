using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplication.DTOs.Request
{
    public class PostUserDto
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}
