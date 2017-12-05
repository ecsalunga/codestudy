using System.Configuration;

namespace Genting.Infrastructure.CommonServices.Client.Core.Configurations
{
    public class ObjectConfiguration : ConfigurationSection
    {
        private static ObjectConfiguration _settings;
        public static ObjectConfiguration Instance
        {
            get
            {
                if (_settings == null)
                    _settings = ConfigurationManager.GetSection("clientManagerObjects") as ObjectConfiguration;

                return _settings;
            }
        }

        [ConfigurationProperty("messenger")]
        public ObjectItem Messenger
        {
            get { return (ObjectItem)this["messenger"]; }
        }

        [ConfigurationProperty("interpreters", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ObjectCollection), AddItemName = "item")]
        public ObjectCollection Interpreters
        {
            get { return (ObjectCollection)this["interpreters"]; }
        }

        [ConfigurationProperty("settings", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ObjectSettings), AddItemName = "item")]
        public ObjectSettings Settings
        {
            get { return (ObjectSettings)this["settings"]; }
        }
    }
}
