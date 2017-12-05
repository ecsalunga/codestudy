using System.Configuration;

namespace Genting.Infrastructure.CommonServices.Client.Core.Configurations
{
    public class ObjectItem : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        [ConfigurationProperty("type", IsKey = true, IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
        }
    }
}
