using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public interface IInterpreter
    {
        ItemState Status { get; }

        string InterpreterType { get; }

        void Translate(IMessage message);

        void SetMessenger(IMessenger messenger);
    }
}