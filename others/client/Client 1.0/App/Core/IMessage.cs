using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public interface IMessage
    {
        string InterpreterType { get; set; }

        string MessageType { get; set; }

        string Payload { get; set; }
    }
}