using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using MyFrameWork.NHib.Filter;

namespace MyFrameWork.NHib.Conventions
{
    public class EntityConvention: IClassConvention
    {
        public void Apply(IClassInstance instance)
        {            
            instance.ApplyFilter<TenantFilter>();
        }
    }
}