using System.Collections.Generic;
using System.Reflection;
using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using MyFrameWork.Domain;
using MyFrameWork.NHib.Conventions;

namespace MyFrameWork.NHib
{
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        public List<Assembly> AutoMappingAssemblies { get; set; }
        public Assembly OverrideAssembly { get; set; }

        public AutoPersistenceModelGenerator()
        {
            AutoMappingAssemblies = new List<Assembly>();
            OverrideAssembly = Assembly.GetAssembly(typeof (DomainEntity));
        }        

        public AutoPersistenceModel Generate()
        {
            return
                AutoMap.Assemblies(new FrameworkMappingConfiguration(), this.AutoMappingAssemblies)
                    .Conventions.Setup(this.GetConventions())
                    .IgnoreBase<DomainEntity>()
                    .UseOverridesFromAssembly(this.OverrideAssembly);
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
                {
                    c.Add<PrimaryKeyConvention>();                    
                    c.Add<TableNameConvention>();
                    //should be used both or none - HasMany and Reference
                    c.Add<HasManyConvention>();
                    c.Add<ReferenceConvention>();
                };
        }
    }
}