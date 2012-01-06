using FluentNHibernate.Automapping;

namespace MyFrameWork.NHib
{    
    public interface IAutoPersistenceModelGenerator
    {
        AutoPersistenceModel Generate();
    }
}