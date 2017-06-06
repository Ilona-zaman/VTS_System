using System;
using Core.CrossCutting;

namespace Core.Business.Exceptions
{
    public class BusinessException : VTSException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
