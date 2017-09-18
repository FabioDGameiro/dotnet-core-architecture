#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Base;

#endregion

namespace Infra.Helpers
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);

            if (items != null) AddRange(items);
        }

        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize, bool metaOnly)
        {
            var count = source.Count();
            var items = metaOnly ? null : source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}