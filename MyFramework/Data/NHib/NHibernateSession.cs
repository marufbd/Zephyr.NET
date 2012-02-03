#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: TestFramework.NHibernate
 * Module: 
 * Name: NHibernateSession
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  12/30/2011 10:08:14 AM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Event;
using NHibernate.Linq;
using Zephyr.Data.NHib.Mapping.Filter;
using Zephyr.Configuration;

#endregion REFERENCES

namespace Zephyr.Data.NHib
{
    public static class NHibernateSession
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        public static ISessionFactory Factory { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public static NHibernate.Cfg.Configuration Configuration { get; private set; }

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <returns></returns>
        public static ISession Initialize(IAutoPersistenceModelGenerator modelGenerator)
        {
            var config = new ZephyrConfig();
            var dataConfig = config.DataConfig();
            var mappingAssemblyNames = dataConfig["MappingAssemblies"] as IList<string>;
            var overrideAssemblyFile = dataConfig["OverrideAssembly"] as string;
            
            var overrideAssembly = Assembly.LoadFrom(overrideAssemblyFile);

            var mappingAssemblies = new List<Assembly>();
            mappingAssemblyNames.ForEach(a => mappingAssemblies.Add(Assembly.LoadFrom(a)));

            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure(dataConfig["NHibConfigPath"] as string);

            FluentConfiguration fConfig = Fluently.Configure(cfg)
                                            .Mappings(m =>
                                                          {
                                                              foreach (var mappingAssembly in mappingAssemblies)
                                                              {
                                                                  m.HbmMappings.AddFromAssembly(mappingAssembly);
                                                              }

                                                              if (modelGenerator == null)
                                                              {
                                                                  //get default persistent model generator
                                                                  var model = new AutoPersistenceModelGenerator();
                                                                  model.AutoMappingAssemblies = mappingAssemblies;
                                                                  model.OverrideAssembly = overrideAssembly;

                                                                  if(config.ExportHbm)
                                                                  {
                                                                      var hbmExportPath = dataConfig["HbmExportPath"] as string;
                                                                      m.AutoMappings.Add(model.Generate).ExportTo(hbmExportPath);
                                                                      m.FluentMappings.Add<TenantFilter>()
                                                                                      .Add<DeletedFilter>()
                                                                                      .ExportTo(hbmExportPath);
                                                                  }
                                                                  else
                                                                  {
                                                                      m.AutoMappings.Add(model.Generate);
                                                                      m.FluentMappings.Add<TenantFilter>()
                                                                                      .Add<DeletedFilter>();
                                                                  }
                                                              }
                                                              else
                                                              {
                                                                  m.AutoMappings.Add(modelGenerator.Generate);
                                                              }
                                                          });

            Configuration = fConfig.BuildConfiguration();

            //Set delete listener for soft delete
            //got to chk with config for this to enable
            Configuration.SetListener(ListenerType.Delete, new EventListeners.SoftDeleteListener());
            //Enable audit on save or update
            Configuration.AppendListeners(ListenerType.Update, new[] { new EventListeners.AuditUpdateListener() });
            Configuration.AppendListeners(ListenerType.SaveUpdate, new[] { new EventListeners.AuditUpdateListener() });

            Factory = Configuration.BuildSessionFactory();
            return Factory.OpenSession();
        }        
    }
}