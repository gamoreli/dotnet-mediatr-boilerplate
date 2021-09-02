using System;

namespace MediatorBoilerplate.Domain.Core.Base.Exceptions
{
    public class CustomValidationException : Exception
    {
        public string Type { get; }
        
        public CustomValidationException(string message, string type) : base(message)
        {
            Type = type;
        }
    }
}