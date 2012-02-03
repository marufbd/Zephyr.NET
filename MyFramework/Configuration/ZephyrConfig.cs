using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web.Hosting;
using NHibernate.Linq;

namespace Zephyr.Configuration
{
    public class ZephyrConfig
    {
        public bool ExportHbm { get; set; }

        public ZephyrConfig()
        {
            var hbmExport = ConfigurationManager.AppSettings["HbmExportPath"] as string;
            ExportHbm = hbmExport != null;
        }

        public IDictionary<string, object> DataConfig()
        {
            IDictionary<string, object> settings=new Dictionary<string, object>();

            IList<string> mappingAssemblies=new List<string>();
            
            ConfigurationManager.AppSettings["MappingAssemblies"].ToString(CultureInfo.InvariantCulture).Split(';')
                .ForEach(s=>mappingAssemblies.Add(MakeLoadReadyAssemblyName(s)));
            

            settings.Add("MappingAssemblies", mappingAssemblies);
            settings.Add("OverrideAssembly", MakeLoadReadyAssemblyName(ConfigurationManager.AppSettings["OverrideAssembly"]));            
            
            if(ExportHbm)
                settings.Add("HbmExportPath", ConfigurationManager.AppSettings["HbmExportPath"]);

            settings.Add("NHibConfigPath", GetNHibConfigPath());

            return settings;
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
            return GetAppPath() + ((assemblyName.IndexOf(".dll", System.StringComparison.Ordinal) == -1) ? assemblyName.Trim() + ".dll" : assemblyName.Trim());
        }
    }
}