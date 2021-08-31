using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> GetById(int entityId);
        public Task<T> Create(T newEntity);
        public T Update(T updateEntity);
        public Task<T> Delete(int entityId);
    }
}
