using Zephyr.Domain;

namespace Zephyr.Web.Mvc.ViewModel
{
    public class ViewModelBase<TEntity> where TEntity:Entity
    {
        public TEntity Model { get; set; }

    }
}