using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplication.DTOs.Response
{
    public class LoggedUserDto
    {
        public LoggedUserDto(string userName, string cpf, string token)
        {
            UserName = userName;
            CPF = cpf;
            Token = token;
            TokenType = Security.Settings.TokenType;
        }

        public string UserName { get; set; }
        public string CPF { get; set; }
        public string TokenType { get; set; }
        public string Token { get; set; }
    }
}
