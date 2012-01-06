using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using MyFrameWork.Domain;

namespace MyFrameWork.NHib
{
    public class FrameworkMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool AbstractClassIsLayerSupertype(Type type)
        {
            return type == typeof(DomainEntity);
        }

        public override bool IsId(Member member)
        {
            return member.Name == "Id";
        }

        public override bool ShouldMap(Type type)
        {
            return
                type.BaseType == typeof(DomainEntity);
        }

        public override bool ShouldMap(Member member)
        {
            return base.ShouldMap(member) && member.CanWrite;
        }
    }
}