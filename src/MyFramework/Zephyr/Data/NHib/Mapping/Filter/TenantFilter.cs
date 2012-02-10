using FluentNHibernate.Mapping;
using NHibernate;

namespace Zephyr.Data.NHib.Mapping.Filter
{
    public class TenantFilter : FilterDefinition
    {
        private static readonly string FILTERNAME = "TenantFilter";
        private static readonly string COLUMNNAME = "TenantId";
        private static readonly string CONDITION = COLUMNNAME + " = :" + COLUMNNAME;
    
        public TenantFilter()
        {
            WithName(FILTERNAME)
              .WithCondition(CONDITION)
              .AddParameter(COLUMNNAME, NHibernateUtil.String);
        }           
    }
}