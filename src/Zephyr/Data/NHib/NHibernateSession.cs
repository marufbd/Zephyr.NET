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
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Linq;
using Zephyr.Data.NHib.EventListeners;
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
        public static NHibernate.Cfg.Configuration Configure(IAutoPersistenceModelGenerator modelGenerator, ZephyrConfiguration frameworkSettings)
        {
            var persistenceConfig = frameworkSettings.PersistenceConfig;
            var overrideAssembly = Assembly.Load(persistenceConfig.OverridingAssembly);

            var mappingAssemblies = new List<Assembly>();
            persistenceConfig.MappingAssemblies.ForEach(a => mappingAssemblies.Add(Assembly.Load(a)));
            //add zephyr by default in the mapping assemblies to map RevisionEntity for NhIbernate envers
            mappingAssemblies.Add(typeof(RevisionEntity).Assembly);

            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure(persistenceConfig.NHibConfigFile);

            FluentConfiguration fConfig = Fluently.Configure(cfg)
                                            .Mappings(m =>
                                            {
                                                //add hbm files from mapping assemblies
                                                mappingAssemblies.ForEach(asm => m.HbmMappings.AddFromAssembly(asm));

                                                if (modelGenerator == null)
                                                {
                                                    //get default persistent model generator
                                                    var model =
                                                        new AutoPersistenceModelGenerator(overrideAssembly)
                                                        {
                                                            AutoMappingAssemblies = mappingAssemblies,
                                                            CoreFrameworkAssembly =
                                                                typeof(ZephyrConfiguration).Assembly
                                                        };


                                                    if (persistenceConfig.HbmExportEnabled)
                                                    {
                                                        var hbmExportPath = persistenceConfig.HbmExportPath;
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
            if (persistenceConfig.LogDiagnosticsEnabled)
                fConfig.Diagnostics(dia => dia.Enable().OutputToFile(persistenceConfig.LogDiagnosticsPath + "/fluentNHibernate.log"));

            //fConfig.ExposeConfiguration(c => c.EventListeners.PreUpdateEventListeners = new[] {new EventListeners.AuditUpdateListener()});

            //Set delete listener for soft delete
            //if (zephyrConfig.SoftDeleteEnabled)
            //    fConfig.ExposeConfiguration(
            //        c => c.SetListener(ListenerType.Delete, new EventListeners.SoftDeleteListener()));


            NHibernate.Cfg.Configuration nhibConfig = fConfig.BuildConfiguration();

            //integrate envers
            //Enable auditing using NHibernate.Envers
            var enversConf = new NHibernate.Envers.Configuration.Fluent.FluentConfiguration();

            enversConf.SetRevisionEntity<RevisionEntity>(e => e.RevNo, e => e.RevisionTimestamp, new NEnversRevInfoListener());

            enversConf.Audit(persistenceConfig.GetDomainModelTypesForAudit());
            //config envers to store deleted information
            nhibConfig.Properties.Add("nhibernate.envers.store_data_at_delete", "true");
            nhibConfig.IntegrateWithEnvers(enversConf);


            return nhibConfig;
        }        
    }
}