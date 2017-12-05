using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genting.Infrastructure.CommonServices.Client.Manager.Models
{
    public class ClientInfo
    {
        public List<string> Interpreters { get; set; }

        public ClientInfo()
        {
            this.Interpreters = new List<string>();
        }
    }
}
