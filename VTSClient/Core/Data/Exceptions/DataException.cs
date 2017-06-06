using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCutting;
using SQLite;

namespace Core.Exceptions
{
    public class DataException : VTSException
    {
        public DataException()
        {
        }

        public DataException(string message) : base(message)
        {
        }

        public DataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
