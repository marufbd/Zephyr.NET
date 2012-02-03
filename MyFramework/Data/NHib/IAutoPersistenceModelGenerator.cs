using FluentNHibernate.Automapping;

namespace Zephyr.Data.NHib
{    
    public interface IAutoPersistenceModelGenerator
    {
        AutoPersistenceModel Generate();
    }
}