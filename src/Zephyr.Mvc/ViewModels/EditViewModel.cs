using Zephyr.Domain;

namespace Zephyr.Web.Mvc.ViewModels
{
    public class EditViewModel<T> : ViewModelBase<T> where T : DomainEntity
    {
        public T Model { get; set; }
    }
}