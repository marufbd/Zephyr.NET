using Zephyr.Domain;

namespace Zephyr.Web.Mvc.ViewModels
{
    public class CreateViewModel<T> : ViewModelBase<T> where T : DomainEntity
    {
        public T Model { get; set; }
        
    }
}