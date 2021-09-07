using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplication.DTOs.Response
{
    public class LoggedUserDto
    {
        public LoggedUserDto()
        {
        }

        public string UserName { get; set; }
        public string CPF { get; set; }
        public string TokenType => Security.Settings.TokenType;
        public string Token { get; set; }
    }
}
