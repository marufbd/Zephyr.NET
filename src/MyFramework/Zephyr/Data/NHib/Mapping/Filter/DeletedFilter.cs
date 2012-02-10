using FluentNHibernate.Mapping;
using NHibernate;

namespace Zephyr.Data.NHib.Mapping.Filter
{
    public class DeletedFilter : FilterDefinition
    {
        private static readonly string FILTERNAME = "DeletedFilter";
        private static readonly string COLUMNNAME = "IsDeleted";
        private static readonly string CONDITION = COLUMNNAME + " = :" + COLUMNNAME;
    
        public DeletedFilter()
        {
            WithName(FILTERNAME)
              .WithCondition(CONDITION)
              .AddParameter(COLUMNNAME, NHibernateUtil.Boolean);
        }
    }
}