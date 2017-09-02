using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public abstract class Parameter
    {
        const int maxPageSize = 25;
        private int _pageSize = 10;
        private string _query;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public string Query
        {
            get { return _query; }
            set { _query = value.Trim().ToLowerInvariant(); }
        }

        public bool HasQuery => !string.IsNullOrWhiteSpace(_query);

        public string Fields { get; set; }
    }
}
