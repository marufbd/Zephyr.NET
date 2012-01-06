using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace MyFrameWork.NHib.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(instance.EntityType.Name.InflectTo().Pluralized);
        }
    }
}