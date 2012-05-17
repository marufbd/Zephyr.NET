using System.Collections.Generic;
using Zephyr.Data.Models;
using Zephyr.Domain;

namespace Zephyr.Web.Mvc.ViewModels
{
    public class ListViewModel<T> : ViewModelBase<T> where T : DomainEntity
    {
        /// <summary>
        /// Gets or sets the model whcih is a <see>
        ///                                       <cref>IPagedList</cref>
        ///                                   </see>
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public IPagedList<T> Model { get; set; }
    }
}