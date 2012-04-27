using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Zephyr.Domain;

namespace Zephyr.Data.NHib.Mapping.Conventions
{
    internal class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            if(instance.EntityType.BaseType==typeof(DomainEntity))
            {                
                //Guid
                instance.Column("Guid");
                //instance.UnsavedValue(Guid.Empty.ToString());
                instance.GeneratedBy.GuidComb();
            }
            else
            {
                instance.Column("Id");
                instance.UnsavedValue("0");
                //instance.GeneratedBy.HiLo("1000");
            }
        }
    }
}