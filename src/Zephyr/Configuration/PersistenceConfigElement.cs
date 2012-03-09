using System.Collections.Generic;
using System.Configuration;

namespace Zephyr.Configuration
{
    public class PersistenceConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("mappingAssemblies")]
        public IList<string> MappingAssemblies {
            get { return this["mappingAssemblies"] as IList<string>; }
            set { this["mappingAssemblies"] = (IList<string>)value.ToString().Split(';'); }
        }
    }
}