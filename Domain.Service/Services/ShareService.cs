using Domain.Model.Entities;
using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class ShareService : IShareService
    {
        private IUnitOfWork _unitOfWork;
        public ShareService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IServiceResult<IEnumerable<Share>>> GetAllShares()
        {
            var shares = await _unitOfWork.ShareRepository.Get().ConfigureAwait(false);
            var serviceResult = new ServiceResult<IEnumerable<Share>>();
            serviceResult.SetResult(shares);
            return serviceResult;
        }
    }
}
