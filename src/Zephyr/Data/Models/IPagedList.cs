using System.Collections.Generic;

namespace Zephyr.Data.Models
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity the PagedList collection should contain.</typeparam>
    public interface IPagedList<out TEntity> : IPagedList, IEnumerable<TEntity>
    {
        TEntity this[int index] { get; }

        /// <summary>
        /// Gets the count of the current subset
        /// </summary>
        int Count { get; }
    }

    public interface IPagedList
    {
        /// <summary>
        /// Gets the page count. 
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// The item count of a single page.
        /// </summary>
        /// <value>
        /// The item count of a single page.
        /// </value>
        int PageSize { get; }

        /// <summary>
        /// Gets the current page number using 1-based index
        /// </summary>
        int PageNumber { get; }


        /// <summary>
        /// Gets the total item count in the collection.
        /// </summary>
        int TotalItemCount { get; }


        /// <summary>
        /// Gets a value indicating whether current page is first page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this page is first page; otherwise, <c>false</c>.
        /// </value>
        bool IsFirstPage { get; }


        /// <summary>
        /// Gets a value indicating whether this page is last page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this page is last page; otherwise, <c>false</c>.
        /// </value>
        bool IsLastPage { get; }
    }
}