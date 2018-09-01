using System;

namespace Web.Exceptions
{
    public class ValidationException : Exception
    {
        public new string Message { get; set; }
        public ValidationException(string message)
        {
            Message = message;
        }
    }
}