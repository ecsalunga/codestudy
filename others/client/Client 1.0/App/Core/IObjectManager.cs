using System.Collections.Generic;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public interface IObjectManager
    {
        Dictionary<string, IInterpreter> Interpreters { get; }
        IMessenger Messenger { get; }

        void Resolve();
    }
}