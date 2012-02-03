using System.Collections.Generic;
using System.Reflection;
using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Zephyr.Data.NHib.Mapping.Conventions;
using Zephyr.Domain;

namespace Zephyr.Data.NHib
{
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {
        public List<Assembly> AutoMappingAssemblies { get; set; }
        public Assembly OverrideAssembly { get; set; }

        public AutoPersistenceModelGenerator()
        {
            AutoMappingAssemblies = new List<Assembly>();
            OverrideAssembly = Assembly.GetAssembly(typeof (Entity));
        }        

        public AutoPersistenceModel Generate()
        {
            return
                AutoMap.Assemblies(new FrameworkMappingConfiguration(), this.AutoMappingAssemblies)
                    .Conventions.Setup(this.GetConventions())
                    .IgnoreBase<Entity>()
                    .UseOverridesFromAssembly(this.OverrideAssembly);
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
                {
                    c.Add<PrimaryKeyConvention>();                    
                    c.Add<TableNameConvention>();
                    c.Add<EntityConvention>();
                    //should be used both or none - HasMany and Reference
                    c.Add<HasManyConvention>();
                    c.Add<ReferenceConvention>();
                };
        }
    }
}