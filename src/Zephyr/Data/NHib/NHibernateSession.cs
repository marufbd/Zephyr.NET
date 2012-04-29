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

using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Linq;
using Zephyr.Data.NHib.Mapping.Filter;
using Zephyr.Configuration;
using Zephyr.Domain;
using Zephyr.Domain.Audit;

using NHibernate.Envers;

#endregion REFERENCES

namespace Zephyr.Data.NHib
{
    public static class NHibernateSession
    {
        private static NHibernate.Cfg.Configuration _configuration;

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        public static ISessionFactory Factory { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public static NHibernate.Cfg.Configuration Configuration
        {
            get {
                if (_configuration == null)
                    Initialize(null);
                
                return _configuration;
            }
        }

        /// <summary>
        /// Initialize. No Argument
        /// </summary>
        /// <returns></returns>
        public static ISession Initialize()
        {
            return Initialize(null);
        }

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <returns></returns>
        public static ISession Initialize(IAutoPersistenceModelGenerator modelGenerator)
        {
            var zephyrConfig = new ZephyrConfig();
            var dataConfig = zephyrConfig.DataConfig;
            var mappingAssemblyNames = dataConfig.MappingAssemblies;
            var overrideAssemblyFile = dataConfig.OverrideAssembly;
            
            var overrideAssembly = Assembly.Load(overrideAssemblyFile);

            var mappingAssemblies = new List<Assembly>();
            mappingAssemblyNames.ForEach(a => mappingAssemblies.Add(Assembly.Load(a)));

            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure(dataConfig.NHibConfigPath);

            FluentConfiguration fConfig = Fluently.Configure(cfg)
                                            .Mappings(m =>
                                                          {
                                                              //add hbm files from mapping assemblies
                                                              mappingAssemblies.ForEach(asm=>m.HbmMappings.AddFromAssembly(asm));

                                                              if (modelGenerator == null)
                                                              {
                                                                  //get default persistent model generator
                                                                  var model =
                                                                      new AutoPersistenceModelGenerator(overrideAssembly)
                                                                          {
                                                                              AutoMappingAssemblies = mappingAssemblies,
                                                                              CoreFrameworkAssembly =
                                                                                  typeof (ZephyrConfiguration).Assembly
                                                                          };


                                                                  if(zephyrConfig.ExportHbm)
                                                                  {
                                                                      var hbmExportPath = dataConfig.HbmExportPath;
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

            //based on config log diagnostics
            if (dataConfig.LogDiagnostics)
                fConfig.Diagnostics(dia =>dia.Enable().OutputToFile(dataConfig.LogPath + "/fluentNHibernate.log"));


            //Enable auditing using NHibernate.Envers
            var enversConf = new NHibernate.Envers.Configuration.Fluent.FluentConfiguration();
            enversConf.Audit(zephyrConfig.GetDomainModelTypes()); 
            //fConfig.ExposeConfiguration(c => c.EventListeners.PreUpdateEventListeners = new[] {new EventListeners.AuditUpdateListener()});
            
            //Set delete listener for soft delete
            if (zephyrConfig.SoftDeleteEnabled)
                fConfig.ExposeConfiguration(
                    c => c.SetListener(ListenerType.Delete, new EventListeners.SoftDeleteListener()));
                                               

            _configuration = fConfig.BuildConfiguration();

            //integrate envers
            _configuration.IntegrateWithEnvers(enversConf);

            
            Factory = _configuration.BuildSessionFactory();
            
            
            return Factory.OpenSession();
        }
    }
}