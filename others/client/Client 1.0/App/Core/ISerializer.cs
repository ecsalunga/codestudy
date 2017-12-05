using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public interface ISerializer
    {
        string Serialize(object info);
        T Deserialize<T>(string data);
    }
}