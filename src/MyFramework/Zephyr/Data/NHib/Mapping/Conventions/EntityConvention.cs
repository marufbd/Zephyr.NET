using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Zephyr.Data.NHib.Mapping.Filter;

namespace Zephyr.Data.NHib.Mapping.Conventions
{
    public class EntityConvention: IClassConvention
    {
        public void Apply(IClassInstance instance)
        {            
            instance.ApplyFilter<TenantFilter>();
            instance.ApplyFilter<DeletedFilter>();
        }
    }
}