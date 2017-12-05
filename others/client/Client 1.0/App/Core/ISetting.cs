using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public interface ISetting
    {
        void Set(string data);

        void Init();

    }
}
