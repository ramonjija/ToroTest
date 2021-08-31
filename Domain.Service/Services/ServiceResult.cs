﻿using Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Service.Services
{
    public class ServiceResult<T> : IServiceResult<T> where T : class
    {
        public List<string> ValidationMessages { get; private set; }
        public T Result { get; private set; }
        public bool Success => !ValidationMessages.Any();

        public ServiceResult()
        {
            ValidationMessages = new List<string>();
        }

        public void AddMessage(string message)
        {
            ValidationMessages.Add(message);
        }
        public void AddMessage(IEnumerable<string> messages)
        {
            ValidationMessages.AddRange(messages);
        }
        public void SetResult(T result)
        {
            Result = result;
        }
    }
}
