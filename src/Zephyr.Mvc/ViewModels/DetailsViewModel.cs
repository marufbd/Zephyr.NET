using Zephyr.Domain;

namespace Zephyr.Web.Mvc.ViewModels
{
    public class DetailsViewModel<T> : ViewModelBase<T> where T : DomainEntity
    {
        public T Model { get; set; }
    }
}