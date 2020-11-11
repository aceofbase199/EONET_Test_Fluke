using System.Collections.Generic;
using System.Linq;

namespace EONET.BL.Models
{
    public class PagedList<T> : List<T>
    {
        public int Start { get; }

        public int PageSize { get; }

        public int Total { get; }

        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> source)
        {
            Total = source.Count();
            AddRange(source);
        }

        public PagedList(IEnumerable<T> source, int start, int pageSize, int total)
        {
            Start = start;
            PageSize = pageSize;
            Total = total;
            AddRange(source);
        }
    }
}
