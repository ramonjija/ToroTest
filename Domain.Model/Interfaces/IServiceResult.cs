using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Interfaces
{
    public interface IServiceResult<T> where T : class
    {
        EntityValidator Validator { get; }
        T Result { get; }
        bool Success { get; }
        void SetResult(T Result);
    }
}
