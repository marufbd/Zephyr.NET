using System.Collections.Generic;
using Zephyr.Domain;

namespace Zephyr.Web.Mvc.ViewModels
{
    public class ListViewModel<T> : ViewModelBase<T> where T : DomainEntity
    {
        public IEnumerable<T> Model { get; set; }
    }
}