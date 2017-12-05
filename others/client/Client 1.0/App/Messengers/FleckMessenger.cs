using Fleck;
using Genting.Infrastructure.CommonServices.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Genting.Infrastructure.CommonServices.Client.Messengers
{
    public class FleckMessenger : IMessenger, ISetting
    {
        public string Path { get; private set; }
        public ItemState Status { get; protected set; }

        WebSocketServer server;
        List<MessagerClient> clients;

        public event Action<IMessage> OnMessage;
        public event Action<IMessage> OnInterMessage;
        public event Action<IMessage> StatusChanged;
        public event Action<MessagerClient> OnClientConnect;

        public void Close()
        {
            clients.ForEach(s => ((IWebSocketConnection)s.Client).Close());
            clients.Clear();
            this.Status = ItemState.Closed;
            this.UpdateStatus("closed");
        }

        public FleckMessenger() { }

        public void Init()
        {
            //"ws://127.0.0.1:8031/service
            if (!string.IsNullOrEmpty(this.Path))
            {
                clients = new List<MessagerClient>();
                server = new WebSocketServer(this.Path);
                FleckLog.Level = LogLevel.Debug;
                server.Start(socket =>
                {
                    socket.OnOpen = () =>
                    {
                        MessagerClient c = addClient(socket);
                        this.UpdateStatus("add", clients.Count.ToString());
                        if (this.OnClientConnect != null)
                            this.OnClientConnect(c);
                    };
                    socket.OnClose = () =>
                    {
                        removeClient(socket);
                        this.UpdateStatus("remove", clients.Count.ToString());
                    };
                    socket.OnMessage = (data) =>
                    {
                        if (this.OnMessage != null)
                        {
                            IMessage message = JsonSerializer.Instance.Deserialize<Message>(data);
                            this.OnMessage(message);
                        }
                    };
                });

                this.Status = ItemState.Initiated;
                this.UpdateStatus("initiated");
            }
            else
            {
                this.Status = ItemState.Error;
                this.UpdateStatus("error");
            }
        }

        public void Send(IMessage message)
        {
            string msg = JsonSerializer.Instance.Serialize(message);
            clients.ForEach(s => ((IWebSocketConnection)s.Client).Send(msg));
        }

        public void Send(string clientId, IMessage message)
        {
            string msg = JsonSerializer.Instance.Serialize(message);
            foreach(var item in clients)
            {
                if (item.Id == clientId)
                    ((IWebSocketConnection)item.Client).Send(msg);
            }
        }

        private void removeClient(IWebSocketConnection client)
        {
            string id = client.ConnectionInfo.Id.ToString();
            if (clients.Exists(c => c.Id == id))
                clients.Remove(GetMessengerClient(id));
        }

        private MessagerClient addClient(IWebSocketConnection client)
        {
            string id = client.ConnectionInfo.Id.ToString();
            MessagerClient c = new MessagerClient(id, client);
            clients.Add(c);
            return c;
        }

        private MessagerClient GetMessengerClient(string clientId)
        {
          return clients.FirstOrDefault(c => c.Id == clientId);
        }

        private void UpdateStatus(string status, string payload="")
        {
            if (this.StatusChanged != null)
            {
                Message msg = new Message() { InterpreterType = "channel", MessageType = status };
                msg.Payload = payload;
                this.StatusChanged(msg);
            }
        }

        public void Set(string config)
        {
            this.Path = config;
            this.Status = ItemState.Default;
            this.UpdateStatus("default");
        }

        public void SendInter(IMessage message)
        {
            if (this.OnInterMessage != null)
                this.OnInterMessage(message);
        }
    }
}