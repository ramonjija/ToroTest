using Domain.Model.Entities;
using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service.Services
{
    public class ServiceResult<T> : IServiceResult<T> where T : class
    {
        public ServiceResult()
        {
            Validator = new EntityValidator();
        }

        public T Result { get; private set; }
        public EntityValidator Validator { get; private set; }
        public bool Success => !Validator.ValidationMessages.Any();

        public void SetResult(T result)
        {
            Result = result;
        }
    }
}
