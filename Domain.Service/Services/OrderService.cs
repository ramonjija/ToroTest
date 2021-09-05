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

        public OrderService(IUnitOfWork unitOfWork, IShareService shareService, IUserPositionService userPositionService)
        {
            _unitOfWork = unitOfWork;
            _userPositionService = userPositionService;
            _shareService = shareService;
        }
        public async Task<IServiceResult<UserPosition>> BuyShare(string shareSymbol, int amount, string userCpf)
        {
            try
            {
                var serviceResult = new ServiceResult<UserPosition>();
                var shareResult = await _shareService.GetShare(shareSymbol).ConfigureAwait(false);
                if (!shareResult.Success)
                {
                    foreach (var validationMessage in shareResult.ValidationMessages)
                    {
                        serviceResult.AddMessage(validationMessage);
                    }
                    return serviceResult;
                }

                var userPositionResult = await _userPositionService.GetUserPosition(userCpf).ConfigureAwait(false);
                if (!userPositionResult.Success)
                {
                    foreach (var validationMessage in userPositionResult.ValidationMessages)
                    {
                        serviceResult.AddMessage(validationMessage);
                    }
                    return serviceResult;
                }

                var share = shareResult.Result;
                var userPosition = userPositionResult.Result;
                var updatedPosition = userPosition.AddPositionToUser(share, amount);
                if (updatedPosition == null)
                {
                    serviceResult.AddMessage("Share could not be bought. Check Account Amount");
                    return serviceResult;
                }

                _unitOfWork.UserPositions.Update(updatedPosition);
                await _unitOfWork.Commit();
                serviceResult.SetResult(updatedPosition);

                return serviceResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
