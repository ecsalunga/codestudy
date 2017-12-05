using System.Collections.Generic;
using System.Configuration;

namespace Genting.Infrastructure.CommonServices.Client.Core.Configurations
{
    public class ObjectCollection : ConfigurationElementCollection, IEnumerable<ObjectItem>
    {
        private readonly List<ObjectItem> items;

        public ObjectCollection()
        {
            this.items = new List<ObjectItem>();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new ObjectItem();
            this.items.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ObjectItem)element).Name;
        }

        public new IEnumerator<ObjectItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }
    }
}
