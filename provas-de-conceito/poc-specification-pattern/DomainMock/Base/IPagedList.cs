using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public interface IPagedList<T>
    {
        int CurrentPage { get; }
        int TotalPages { get; }
        int PageSize { get; }
        int TotalCount { get; }

        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}
