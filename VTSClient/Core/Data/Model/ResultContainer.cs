using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Model
{
    public class ResultContainer<T>: ResultContainer
    {
        public T ItemResult { get; set; }
        public T ListResult { get; set; }

    }

    public class ResultContainer
    {
        public string Result { get; set; }
        public int ResultCode { get; set; }

    }
}
