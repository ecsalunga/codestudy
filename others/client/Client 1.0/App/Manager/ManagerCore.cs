using Genting.Infrastructure.CommonServices.Client.Configurations.Autofac;
using Genting.Infrastructure.CommonServices.Client.Core;
using Genting.Infrastructure.CommonServices.Client.Manager.Models;

namespace Genting.Infrastructure.CommonServices.Client.Manager
{
    public class ManagerCore
    {
        ClientInfo _client;

        private IObjectManager _objectPool;
        public void Init()
        {
            _objectPool = ObjectManager.Instance;
            //_objectPool = AutofacObjectManager.Instance;
            _objectPool.Resolve();
            _objectPool.Messenger.OnMessage += Messenger_OnMessage;
            _objectPool.Messenger.OnInterMessage += Messenger_OnInterMessage;
            _objectPool.Messenger.OnClientConnect += Messenger_OnClientConnect;

            _client = new ClientInfo();
            foreach (string key in _objectPool.Interpreters.Keys)
            {
                IInterpreter interpreter = _objectPool.Interpreters[key];
                _client.Interpreters.Add(interpreter.InterpreterType);
            }
        }

        private void Messenger_OnClientConnect(MessagerClient client)
        {
            IMessage cmMessage = Message.CreateMessage("ClientManager", _client, "CLIENT");
            _objectPool.Messenger.Send(client.Id, cmMessage);
        }

        private void Messenger_OnInterMessage(IMessage msg)
        {
            this.translate(msg);
        }

        private void Messenger_OnMessage(IMessage msg)
        {
            this.translate(msg);
        }

        private void translate(IMessage msg)
        {
            foreach (string key in _objectPool.Interpreters.Keys)
            {
                IInterpreter interpreter = _objectPool.Interpreters[key];

                if (interpreter.InterpreterType == msg.InterpreterType)
                    interpreter.Translate(msg);
            }
        }
    }
}
