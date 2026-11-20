using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items,int totalCount,int pageSize,int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            TotlaPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            ItemsFrom = pageSize * (pageNumber - 1)+1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalItemsCount { get; set; }
        public int TotlaPage { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
