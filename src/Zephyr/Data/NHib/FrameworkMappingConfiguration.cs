using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using Zephyr.Domain;
using System.Linq;
using Zephyr.Domain.Audit;

namespace Zephyr.Data.NHib
{
    public class FrameworkMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool AbstractClassIsLayerSupertype(Type type)
        {
            return type == typeof(EntityWithTypedId<>) || type == typeof(Entity) || type==typeof(DomainEntity);
        }

        public override bool IsId(Member member)
        { 
            return member.Name == "Id";
        }

        public override bool ShouldMap(Type type)
        {
            return 
                type.GetInterfaces().Any(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IEntityWithTypedId<>));
        }

        public override bool ShouldMap(Member member)
        {
            return base.ShouldMap(member) && member.CanWrite && !member.Name.Equals("IsNew");
        }
    }
}