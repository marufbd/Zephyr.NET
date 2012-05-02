using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Web.Hosting;
using NHibernate.Linq;
using System.Linq;
using Zephyr.Domain;
using Zephyr.Domain.Audit;

namespace Zephyr.Configuration
{
    public class ZephyrConfig
    {
        public bool ExportHbm { get; set; }
        public bool ExportDbSchema { get; set; }
        public bool SoftDeleteEnabled { get; set; }

        public ZephyrDataConfig DataConfig
        {
            get { return this.GetDataConfig(); }
        }

        public ZephyrConfig()
        {
            var hbmExport = ConfigurationManager.AppSettings["HbmExportPath"] as string;
            var sqlSchemaExport = ConfigurationManager.AppSettings["DbSchemaExportPath"] as string;
            var softDelete = ConfigurationManager.AppSettings["SoftDeleteEnabled"] as string;

            SoftDeleteEnabled = softDelete != null && Convert.ToBoolean(softDelete);

            ExportHbm = hbmExport != null;
            ExportDbSchema = sqlSchemaExport != null;
        }

        private ZephyrDataConfig GetDataConfig()
        {            
            IDictionary<string, object> settings=new Dictionary<string, object>();

            IList<string> mappingAssemblies=new List<string>();

            var asmNames =
                ConfigurationManager.AppSettings["MappingAssemblies"].ToString(CultureInfo.InvariantCulture).Split(';').
                    Where(n => !String.IsNullOrEmpty(n)).ToList();
            asmNames.Add(typeof(ZephyrConfiguration).Assembly.FullName);
            asmNames.Distinct().ForEach(mappingAssemblies.Add);
            
            settings.Add("MappingAssemblies", mappingAssemblies);
            settings.Add("OverrideAssembly", ConfigurationManager.AppSettings["OverrideAssembly"]);
            
            if(ExportHbm)
                settings.Add("HbmExportPath", ConfigurationManager.AppSettings["HbmExportPath"]);
            if (ExportDbSchema)
                settings.Add("DbSchemaExportPath", ConfigurationManager.AppSettings["DbSchemaExportPath"]);

            var mapConfigLogPath = ConfigurationManager.AppSettings["MappingConfigLogPath"] as string;
            if (mapConfigLogPath!=null)
            {                
                settings.Add("MappingConfigLogPath", mapConfigLogPath);
            }            

            settings.Add("NHibConfigPath", GetNHibConfigPath());

            return new ZephyrDataConfig(settings);
        } 

        private string GetNHibConfigPath()
        {
            return GetAppPath() + ConfigurationManager.AppSettings["NHibConfigFile"];
        }

        private string GetAppPath()
        {
            if (ApplicationManager.GetApplicationManager().GetRunningApplications().Length > 0)
                return ApplicationManager.GetApplicationManager().GetRunningApplications()[0].PhysicalPath + "bin\\";
            else
            {
                return string.Empty;
            }
        }

        private string MakeLoadReadyAssemblyName(string assemblyName)
        {            
            if (assemblyName.EndsWith(".exe"))
                return assemblyName.Trim();

            return ((assemblyName.IndexOf(".dll", System.StringComparison.Ordinal) == -1) ? assemblyName.Trim() + ".dll" : assemblyName.Trim());
        }

        public IEnumerable<Type> GetDomainModelTypes()
        {
            var lst = new List<Type>();
            foreach (var asmName in DataConfig.MappingAssemblies)
            {
                Assembly asm = Assembly.Load(asmName);
                lst.AddRange(asm.GetTypes().Where(type => type.GetInterfaces().Any(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IEntityWithTypedId<>)) &&
                                                          !type.GetInterfaces().Contains(typeof (IRevisionEntity))));
            }

            return lst;
        }
    }

    public class ZephyrDataConfig
    {
        public IList<string> MappingAssemblies { get; protected set; }
        public string OverrideAssembly { get; protected set; }
        public string NHibConfigPath { get; protected set; }
        
        public string LogPath { get; protected set; }
        public bool LogDiagnostics { get; protected set; }


        public string HbmExportPath { get; protected set; }
        public string DbSchemaExportPath { get; protected set; }

        public ZephyrDataConfig(IDictionary<string, object> settings)
        {
            this.MappingAssemblies = settings["MappingAssemblies"] as IList<string>;
            this.OverrideAssembly = settings["OverrideAssembly"] as string;
            this.NHibConfigPath = settings["NHibConfigPath"] as string;
            

            this.HbmExportPath = settings.ContainsKey("HbmExportPath") ? settings["HbmExportPath"] as string:"";
            this.DbSchemaExportPath = settings.ContainsKey("DbSchemaExportPath")
                                          ? settings["DbSchemaExportPath"] as string
                                          : "";

            this.LogDiagnostics = settings.ContainsKey("MappingConfigLogPath") ? true : false;
            if (LogDiagnostics)
            {
                this.LogPath = settings["MappingConfigLogPath"] as string;
            }
                
        }
    }
}