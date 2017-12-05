using System;
using System.Collections.Generic;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public class Message : IMessage
    {
        public string InterpreterType { get; set; }

        public string MessageType { get; set; }

        public string Payload { get; set; }
       
        public static IMessage CreateMessage(string interpreterType, object info, string messegeType = "default")
        {
            Message msg = new Message() { InterpreterType = interpreterType, MessageType = messegeType };
            msg.Payload = JsonSerializer.Instance.Serialize(info);
            return msg;
        }
    }
}