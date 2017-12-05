using System.Configuration;

namespace Genting.Infrastructure.CommonServices.Client.Core.Configurations
{
    public class ObjectSetting : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        [ConfigurationProperty("data", IsKey = true, IsRequired = true)]
        public string Data
        {
            get { return (string)this["data"]; }
        }
    }
}
