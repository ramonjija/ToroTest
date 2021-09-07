using Domain.Model.Aggregate;
using Domain.Model.Entities;
using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IUserPositionService _userPositionService;
        private IShareService _shareService;
        private IUserService _userService;


        public OrderService(IUnitOfWork unitOfWork, IShareService shareService, IUserPositionService userPositionService, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userPositionService = userPositionService;
            _shareService = shareService;
            _userService = userService;
        }
        public async Task<IServiceResult<UserPosition>> BuyShare(string shareSymbol, int amount, string userCpf)
        {
            try
            {
                var serviceResult = new ServiceResult<UserPosition>();
                var userServiceResult = await _userService.GetUser(userCpf).ConfigureAwait(false);
                if (!userServiceResult.Success)
                {
                    serviceResult.Validator.AddMessage(userServiceResult.Validator.ValidationMessages);
                    return serviceResult;
                }

                var shareResult = await _shareService.GetShare(shareSymbol).ConfigureAwait(false);
                if (!shareResult.Success)
                {
                    serviceResult.Validator.AddMessage(shareResult.Validator.ValidationMessages);
                    return serviceResult;
                }

                var userPositionResult = await _userPositionService.GetUserPosition(userCpf).ConfigureAwait(false);
                if (!userPositionResult.Success)
                {
                    serviceResult.Validator.AddMessage(userPositionResult.Validator.ValidationMessages);
                    return serviceResult;
                }

                var share = shareResult.Result;
                var userPosition = userPositionResult.Result;
                var updatedPosition = userPosition.AddPositionToUser(share, amount);
                if (!updatedPosition.Validator.IsValid)
                {
                    serviceResult.Validator.AddMessage(updatedPosition.Validator.ValidationMessages);
                    return serviceResult;
                }

                _unitOfWork.UserPositions.Update(updatedPosition);
                await _unitOfWork.Commit();
                serviceResult.SetResult(updatedPosition);

                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IServiceResult<UserPosition>> AddBalance(decimal addedBalance, string userCpf)
        {
            try
            {
                var serviceResult = new ServiceResult<UserPosition>();
                var userServiceResult = await _userService.GetUser(userCpf).ConfigureAwait(false);
                if (!userServiceResult.Success)
                {
                    foreach (var validationMessage in userServiceResult.Validator.ValidationMessages)
                    {
                        serviceResult.Validator.AddMessage(validationMessage);
                    }
                    return serviceResult;
                }

                var user = userServiceResult.Result;
                var userPositionResult = await _userPositionService.GetUserPosition(userCpf).ConfigureAwait(false);
                if (!userPositionResult.Success)
                {
                    var newUserPosition = new UserPosition(null, addedBalance, user);
                    await _unitOfWork.UserPositions.Create(newUserPosition);
                    serviceResult.SetResult(newUserPosition);
                }
                else
                {
                    var userPosition = userPositionResult.Result;
                    var updatedPosition = userPosition.AddBalance(addedBalance);
                    _unitOfWork.UserPositions.Update(updatedPosition);
                    serviceResult.SetResult(updatedPosition);
                }
                await _unitOfWork.Commit();

                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
