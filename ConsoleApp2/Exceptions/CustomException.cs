
using System;

namespace ConsoleApp2.Exceptions
{
    public class CustomException : Exception
    {
        public virtual int ErrorCode { get; set; }
        public virtual string ErrorMessage { get; set; } = string.Empty;
    }
}
