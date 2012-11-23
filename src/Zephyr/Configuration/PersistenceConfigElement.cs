using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using Zephyr.Domain;
using Zephyr.Domain.Audit;

namespace Zephyr.Configuration
{
    public class PersistenceConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("mappingAssemblies")]
        private string _MappingAssemblies {
            get { return (string)this["mappingAssemblies"]; }
            set { this["mappingAssemblies"] = value; }
        }

        public IList<string> MappingAssemblies { get { return this._MappingAssemblies.Split(';').ToList(); } } 

        [ConfigurationProperty("overridingAssembly")]
        public string OverridingAssembly
        {
            get { return (string)this["overridingAssembly"]; }
            set { this["overridingAssembly"] = value; }
        }

        [ConfigurationProperty("nhibConfigFile")]
        public string NHibConfigFile
        {
            get { return GetAppPath() + (string) this["nhibConfigFile"]; }
            set { this["nhibConfigFile"] = value; }
        }

        [ConfigurationProperty("softDeleteEnabled", DefaultValue = false, IsRequired = false)]
        public bool SoftDeleteEnabled
        {
            get { return (bool)this["softDeleteEnabled"]; }
            set { this["softDeleteEnabled"] = value; }
        }

        [ConfigurationProperty("dbSchemaExportPath", DefaultValue = @"", IsRequired = false)]
        public string DbSchemaExportPath
        {
            get { return (string) this["dbSchemaExportPath"]; }
            set { this["dbSchemaExportPath"] = value; }
        }

        [ConfigurationProperty("hbmExportPath", DefaultValue = @"", IsRequired = false)]
        public string HbmExportPath
        {
            get { return (string)this["hbmExportPath"]; }
            set { this["hbmExportPath"] = GetAppPath() + value; }
        }

        [ConfigurationProperty("logDiagnosticsPath", DefaultValue = @"", IsRequired = false)]
        public string LogDiagnosticsPath
        {
            get { return (string)this["logDiagnosticsPath"]; }
            set { this["logDiagnosticsPath"] = value; }
        }

        public bool HbmExportEnabled { get { return !string.IsNullOrEmpty(HbmExportPath) && Directory.Exists(HbmExportPath); } }
        public bool DbSchemaExportEnabled { get { return !string.IsNullOrEmpty(DbSchemaExportPath) && Directory.Exists(DbSchemaExportPath); } }
        public bool LogDiagnosticsEnabled { get { return !string.IsNullOrEmpty(LogDiagnosticsPath) && Directory.Exists(LogDiagnosticsPath); } }


        public IEnumerable<Type> GetDomainModelTypesForAudit()
        {
            var lst = new List<Type>();
            foreach (var asmName in MappingAssemblies)
            {
                Assembly asm = Assembly.Load(asmName);
                lst.AddRange(asm.GetTypes().Where(type => type.GetInterfaces().Any(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>)) &&
                                                          !type.GetInterfaces().Contains(typeof(IRevisionEntity))));
            }

            return lst;
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
    }
}