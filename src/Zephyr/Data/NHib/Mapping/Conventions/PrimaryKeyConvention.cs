using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Zephyr.Domain;
using Zephyr.Domain.Audit;

namespace Zephyr.Data.NHib.Mapping.Conventions
{
    internal class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column("Id");

            if(instance.EntityType.BaseType==typeof(DomainEntity))
            {                
                //instance.UnsavedValue(Guid.Empty.ToString());
                instance.GeneratedBy.GuidComb();
            }
            else if (instance.EntityType.BaseType==typeof(Entity))
            { 
                instance.UnsavedValue("0");
                //instance.GeneratedBy.HiLo("1000");
            }
        }
    }
}