using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public class MessagerClient
    {
        public string Id { get; set; }
        public object Client { get; set; }
        public MessagerClient(string id, object client)
        {
            this.Id = id;
            this.Client = client;
        }
    }
}
