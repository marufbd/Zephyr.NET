using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Zephyr.Data.NHib.Mapping.Filter;

namespace Zephyr.Data.NHib.Mapping.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.ApplyFilter<DeletedFilter>();

            instance.Key.Column(instance.EntityType.Name + "Fk");

            //demonstrate business requirement - all has many relation should be fethced eagerly
            //to alleviate classic N+1 problem when iterating an "entity list property" and 
            //access properties on each iterator item
            //set batch 5 to eager fetch at most 5 entities when accessing a collection property.
            //its a good candidate for a general purpose query performance tuning
            //Note: the global class level adonet.batch_size can be set in nhibernate.config file
            instance.BatchSize(5);

            instance.Cascade.All();
        }
    }
}