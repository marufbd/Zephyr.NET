using System.Linq;

namespace Zephyr.Data.Models
{
    public class PagedList<TEntity> : BasePagedList<TEntity>
    {
        public PagedList(IQueryable<TEntity> superSet, int pageNumber, int pageSize, int totalItemCount) : base(pageNumber, pageSize, totalItemCount)
        {
            // the paging query is executed on queryable here on ToList() call
            if(superSet!=null && totalItemCount>0)
            {
                Subset.AddRange(pageNumber == 1
                                    ? superSet.Skip(0).Take(pageSize).ToList()
                                    : superSet.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToList());
            }
        }
    }
}