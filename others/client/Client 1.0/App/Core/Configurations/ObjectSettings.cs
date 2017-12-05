using System.Collections.Generic;
using System.Configuration;

namespace Genting.Infrastructure.CommonServices.Client.Core.Configurations
{
    public class ObjectSettings : ConfigurationElementCollection, IEnumerable<ObjectSetting>
    {
        private readonly List<ObjectSetting> items;

        public ObjectSettings()
        {
            this.items = new List<ObjectSetting>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new ObjectSetting();
            this.items.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ObjectSetting)element).Name;
        }

        public new IEnumerator<ObjectSetting> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}
