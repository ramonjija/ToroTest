using Domain.Model.Aggregate;
using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class UserPositionService : IUserPositionService
    {
        private IUnitOfWork _unitOfWork;
        public UserPositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IServiceResult<UserPosition>> GetUserPosition(string cpf)
        {
            try
            {
                var serviceResult = new ServiceResult<UserPosition>();
                var userPosition = await _unitOfWork.UserPositions.GetPositionByCpf(cpf).ConfigureAwait(false);
                if (userPosition == null)
                {
                    serviceResult.AddMessage("User Position not found");
                    return serviceResult;
                }
                serviceResult.SetResult(userPosition);
                return serviceResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
