using Domain.Model.Entities;
using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IPasswordService _passwordService;

        public UserService(IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task<IServiceResult<User>> CreateUser(string name, string cpf, string password)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();

                var existingUser = await _unitOfWork.Users.GetUserByCpf(cpf);
                if (existingUser != null)
                {
                    serviceResult.AddMessage($"There's already a user with this CPF. 'CPF: {cpf}'");
                }

                if (!serviceResult.Success)
                    return serviceResult;

                if(!IsCpfValid(cpf))
                {
                    serviceResult.AddMessage($"The CPF format is invalid. 'CPF: {cpf}'");
                }

                if (!serviceResult.Success)
                    return serviceResult;

                var user = new User(cpf, name, _passwordService.HashPassword(password));

                var newUser = await _unitOfWork.Users.Create(user);
                await _unitOfWork.Commit();
                serviceResult.SetResult(newUser);

                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IServiceResult<User>> DeleteUser(int userId)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var deletedUser = await _unitOfWork.Users.Delete(userId);
                if (deletedUser == null)
                {
                    serviceResult.AddMessage("User Not Found");
                    return serviceResult;
                }
                await _unitOfWork.Commit();
                serviceResult.SetResult(deletedUser);
                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IServiceResult<User>> GetUser(int userId)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var user = await _unitOfWork.Users.GetById(userId);
                if (user == null)
                {
                    serviceResult.AddMessage("User Not Found");
                    return serviceResult;
                }
                serviceResult.SetResult(user);
                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IServiceResult<User>> GetUser(string cpf, string password)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var user = await _unitOfWork.Users.GetUserByCpf(cpf);
                if (user == null)
                {
                    serviceResult.AddMessage("User Not Found");
                    return serviceResult;
                }
                if (!_passwordService.IsPasswordValid(password, user.PasswordHash))
                {
                    serviceResult.AddMessage("Incorrect Password");
                    return serviceResult;
                }

                serviceResult.SetResult(user);
                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IServiceResult<IEnumerable<User>>> GetUser()
        {
            try
            {
                var serviceResult = new ServiceResult<IEnumerable<User>>();
                var users = await _unitOfWork.Users.Get();
                serviceResult.SetResult(users);
                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IServiceResult<User>> UpdateUser(int userId, string userName, string password, int TypeId)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var user = await _unitOfWork.Users.GetById(userId);
                if (user == null)
                {
                    serviceResult.AddMessage("User Not Found");
                    return serviceResult;
                }
                serviceResult.SetResult(user);
                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
