using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Zephyr.Domain.Audit;

namespace Zephyr.Domain.MappingOverrides
{
    public class AuditChangeLogMappingOverride : IAutoMappingOverride<AuditChangeLog>
    {
        public void Override(AutoMapping<AuditChangeLog> mapping)
        {
            mapping.Map(x => x.OldPropertyValue).Length(5000);
            mapping.Map(x => x.NewPropertyValue).Length(5000);
        }
    }
}