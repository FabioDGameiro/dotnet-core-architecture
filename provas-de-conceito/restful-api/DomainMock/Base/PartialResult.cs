using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public class PartialResult<T>
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PartialResult(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
    }
}
