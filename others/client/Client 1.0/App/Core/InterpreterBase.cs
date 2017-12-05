using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public abstract class InterpreterBase : IInterpreter
    {
        protected IMessenger Messenger { get; set; }

        public string InterpreterType { get; protected set; }

        public ItemState Status { get; protected set; }

        public virtual void Translate(IMessage message)
        {
            IMessage msg = this.CreateMessage("not supported");
            this.Send(msg);
        }

        public virtual void SetMessenger(IMessenger messenger)
        {
            this.Messenger = messenger;
            this.Status = ItemState.Initiated;
        }

        protected virtual void Send(IMessage message)
        {
            this.Messenger.Send(message);
        }

        protected virtual IMessage CreateMessage(object info, string messegeType = "default")
        {
            return Message.CreateMessage(this.InterpreterType, info, messegeType);
        }
    }
}