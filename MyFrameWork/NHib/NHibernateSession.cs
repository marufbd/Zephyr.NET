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
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Web.Hosting;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Linq;

#endregion REFERENCES

namespace MyFrameWork.NHib
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
            string[] mappingAssemblyNames = ConfigurationManager.AppSettings["MappingAssemblies"].ToString(CultureInfo.InvariantCulture).Split(';');
            string overrideAssemblyName  = ConfigurationManager.AppSettings["OverrideAssembly"];
            string hbmExportPath = ConfigurationManager.AppSettings["HbmExportPath"];

            var overrideAssembly = Assembly.LoadFrom(MakeLoadReadyAssemblyName(overrideAssemblyName));

            var mappingAssemblies = new List<Assembly>();
            mappingAssemblyNames.ForEach(a => mappingAssemblies.Add(Assembly.LoadFrom(MakeLoadReadyAssemblyName(a))));

            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure(GetNHibConfigPath());

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

                                                                  m.AutoMappings.Add(model.Generate).ExportTo(hbmExportPath);
                                                              }
                                                              else
                                                              {
                                                                  m.AutoMappings.Add(modelGenerator.Generate);
                                                              }                                                              
                                                          });

            Configuration = fConfig.BuildConfiguration();

            Factory = Configuration.BuildSessionFactory();
            return Factory.OpenSession();
        }

        private static string GetNHibConfigPath()
        {
            return GetAppPath() + ConfigurationManager.AppSettings["NHibConfigFile"];
        }

        private static string GetAppPath()
        {
            if (ApplicationManager.GetApplicationManager().GetRunningApplications().Length > 0)
                return ApplicationManager.GetApplicationManager().GetRunningApplications()[0].PhysicalPath + "bin\\";
            else
            {
                return string.Empty;
            }
        }

        private static string MakeLoadReadyAssemblyName(string assemblyName)
        {
            return GetAppPath() + ((assemblyName.IndexOf(".dll", System.StringComparison.Ordinal) == -1) ? assemblyName.Trim() + ".dll" : assemblyName.Trim());
        }
    }
}