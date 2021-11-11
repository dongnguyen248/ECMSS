using System.Collections.Generic;

namespace ECMSS.Web.Extensions.Core
{
    public class PaginationSet<TEntity>
    {
        public int PageIndex { set; get; }
        public int PageSize { get; set; }
        public int TotalRows { set; get; }
        public IEnumerable<TEntity> Items { set; get; }
    }
}