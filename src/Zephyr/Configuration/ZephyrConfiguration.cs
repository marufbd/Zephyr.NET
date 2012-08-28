using System.Configuration;

namespace Zephyr.Configuration
{
    public class ZephyrConfiguration : ConfigurationSection
    {
        public static ZephyrConfiguration ZephyrSettings
        {
            get { return ConfigurationManager.GetSection("Zephyr") as ZephyrConfiguration; }
        }

        [ConfigurationProperty("xmlns")]
        public string XmlNamespace
        {
            get { return (string)this["xmlns"]; }
            set { this["xmlns"] = value; }
        }

        [ConfigurationProperty("appName")]
        public string AppName {
            get { return (string) this["appName"]; }
            set { this["appName"] = value; }
        }

        [ConfigurationProperty("persistence")]
        public PersistenceConfigElement PersistenceConfig
        {
            get { return (PersistenceConfigElement)this["persistence"]; }
            set { this["persistence"] = value; }
        }
    }
}