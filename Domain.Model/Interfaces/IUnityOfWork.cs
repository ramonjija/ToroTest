using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserPositionRepository UserPositions { get; }
        IPositionRepository PositionRepository { get; }
        IShareRepository ShareRepository { get; }

        Task Commit();
    }
}
