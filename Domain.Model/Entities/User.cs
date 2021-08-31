using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string cpf, string userName, string passwordHash)
        {
            CPF = cpf;
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public int UserId { get; protected set; }
        public string CPF { get; protected set; }
        public string UserName { get; protected set; }
        public string PasswordHash { get; protected set; }

        public void Update(string userName, string passwordHash)
        {
            UserName = userName;
            PasswordHash = passwordHash;
        }

    }
}
