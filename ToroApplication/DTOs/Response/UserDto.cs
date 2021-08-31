using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplication.DTOs.Response
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CPF { get; set; }
    }
}
