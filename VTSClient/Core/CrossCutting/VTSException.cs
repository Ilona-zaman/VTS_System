using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutting
{
    public class VTSException : Exception
    {
        public VTSException()
        {
        }

        public VTSException(string message) : base(message)
        {
        }

        public VTSException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
