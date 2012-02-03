using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using Zephyr.Domain;

namespace Zephyr.Data.NHib
{
    public class FrameworkMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool AbstractClassIsLayerSupertype(Type type)
        {
            return type == typeof(EntityWithTypedId<>);
        }

        public override bool IsId(Member member)
        {
            return member.Name == "Id";
        }

        public override bool ShouldMap(Type type)
        {            
            return
                type.IsSubclassOf(typeof(EntityWithTypedId<int>));
        }

        public override bool ShouldMap(Member member)
        {
            return base.ShouldMap(member) && member.CanWrite;
        }
    }
}