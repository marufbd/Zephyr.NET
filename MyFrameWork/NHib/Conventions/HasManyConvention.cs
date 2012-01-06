using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace MyFrameWork.NHib.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name + "Fk");
            instance.Inverse();
        }
    }
}