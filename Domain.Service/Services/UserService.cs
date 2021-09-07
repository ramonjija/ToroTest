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

        public async Task<IServiceResult<User>> CreateUser(string cpf, string name, string password)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();

                var existingUser = await _unitOfWork.Users.GetUserByCpf(cpf);
                if (existingUser != null)
                {
                    serviceResult.Validator.AddMessage($"There's already a user with this CPF. 'CPF: {cpf}'");
                    return serviceResult;
                }

                var user = new User(cpf, name, _passwordService.HashPassword(password));
                if(!user.Validator.IsValid)
                {
                    serviceResult.Validator.AddMessage(user.Validator.ValidationMessages);
                    return serviceResult;
                }

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

        public async Task<IServiceResult<User>> GetUser(string cpf, string password)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var user = await _unitOfWork.Users.GetUserByCpf(cpf);
                if (user == null)
                {
                    serviceResult.Validator.AddMessage("User Not Found");
                    return serviceResult;
                }

                if (!_passwordService.IsPasswordValid(password, user.PasswordHash))
                {
                    serviceResult.Validator.AddMessage("Incorrect Password");
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

        public async Task<IServiceResult<User>> DeleteUser(int userId)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var deletedUser = await _unitOfWork.Users.Delete(userId);
                if (deletedUser == null)
                {
                    serviceResult.Validator.AddMessage("User Not Found");
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
                    serviceResult.Validator.AddMessage("User Not Found");
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
        
        public async Task<IServiceResult<User>> GetUser(string cpf)
        {
            try
            {
                var serviceResult = new ServiceResult<User>();
                var user = await _unitOfWork.Users.GetUserByCpf(cpf);
                if (user == null)
                {
                    serviceResult.Validator.AddMessage("User Not Found");
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
                    serviceResult.Validator.AddMessage("User Not Found");
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
    }
}
