using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Model.Entities
{
    public class User
    {
        private string cPF;

        public User()
        {
            Validator = new EntityValidator();
        }

        public User(string cpf, string userName, string passwordHash)
        {
            Validator = new EntityValidator();
            CPF = cpf;
            UserName = userName;
            PasswordHash = passwordHash;
        }
        
        [NotMapped]
        public EntityValidator Validator { get; private set; }

        public int UserId { get; protected set; }
        public string CPF
        {
            get => cPF;
            protected set
            {
                cPF = value;
                if (IsCpfValid(value))
                {
                    cPF = value.Replace(".", "").Replace("-", "");
                }
                else
                {
                    Validator.AddMessage($"The CPF's format is invalid. 'CPF: {cPF}'");
                }
            }
        }
        public string UserName { get; protected set; }
        public string PasswordHash { get; protected set; }

        public void Update(string userName, string passwordHash)
        {
            UserName = userName;
            PasswordHash = passwordHash;
        }

        private bool IsCpfValid(string cpf)
        {
            int[] firstMultiplier = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondMultiplier = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * firstMultiplier[i];

            int residue = sum % 11;
            if (residue < 2)
                residue = 0;
            else
                residue = 11 - residue;

            string digit = residue.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * secondMultiplier[i];

            residue = sum % 11;
            if (residue < 2)
                residue = 0;
            else
                residue = 11 - residue;

            digit = digit + residue.ToString();

            return cpf.EndsWith(digit);
        }

    }
}
