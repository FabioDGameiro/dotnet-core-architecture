namespace Domain.Base
{
    public abstract class Parameter
    {
        private const int MaxPageSize = 25;

        private string _orderBy;
        private int _pageSize = 10;

        private string _query;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string Query
        {
            get => _query;
            set => _query = value.Trim().ToLowerInvariant();
        }

        public bool HasQuery => !string.IsNullOrWhiteSpace(_query);

        public string OrderBy
        {
            get => _orderBy;
            set => _orderBy = value.Trim().ToLowerInvariant();
        }

        public string Fields { get; set; }

        public bool MetaOnly { get; set; }
    }
}