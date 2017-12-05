using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public interface IMessenger
    {
        event Action<IMessage> OnMessage;

        event Action<IMessage> OnInterMessage;

        event Action<MessagerClient> OnClientConnect;

        event Action<IMessage> StatusChanged;

        ItemState Status { get; }

        void Init();

        void Send(IMessage message);

        void Send(string clientId, IMessage message);

        void SendInter(IMessage message);

        void Close();
    }
}