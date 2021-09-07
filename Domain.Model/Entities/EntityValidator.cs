using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model.Entities
{
    public class EntityValidator
    {
        public EntityValidator()
        {
            ValidationMessages = new List<string>();
        }

        public List<string> ValidationMessages { get; private set; }
        public bool IsValid => !ValidationMessages.Any();

        public void AddMessage(string message)
        {
            ValidationMessages.Add(message);
        }

        public void AddMessage(IEnumerable<string> messages)
        {
            ValidationMessages.AddRange(messages);
        }
    }
}
