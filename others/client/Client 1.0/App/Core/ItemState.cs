using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public enum ItemState
    {
        Default = 1,
        Initiated,
        Running,
        Paused,
        Closed,
        Error
    }
}