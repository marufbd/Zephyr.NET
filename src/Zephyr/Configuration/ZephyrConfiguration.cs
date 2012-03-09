using System.Configuration;

namespace Zephyr.Configuration
{
    public class ZephyrConfiguration : ConfigurationSection
    {
        public static ZephyrConfiguration ZephyrSettings
        {
            get { return ConfigurationManager.GetSection("zephyr/zephyr-config") as ZephyrConfiguration; }
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