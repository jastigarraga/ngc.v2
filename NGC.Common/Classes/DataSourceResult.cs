using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Common.Classes
{
    public class DataSourceResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }
    }
}
