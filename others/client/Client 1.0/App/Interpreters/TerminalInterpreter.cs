using Genting.Infrastructure.CommonServices.Client.Core;
using Genting.Infrastructure.CommonServices.Client.Interpreters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Genting.Infrastructure.CommonServices.Client.Interpreters
{
    public class TerminalInterpreter : InterpreterBase
    {
        public string _mac;
        public TerminalInterpreter()
        {
            this.InterpreterType = "terminal";
            this.Status = ItemState.Default;
        }

        public override void Translate(IMessage message)
        {
            if(message.MessageType == "INFO")
            {
                if(string.IsNullOrEmpty(_mac))
                {
                    NetworkInterface network = NetworkInterface.GetAllNetworkInterfaces().Where(net => net.OperationalStatus == OperationalStatus.Up).FirstOrDefault();
                    if (network != null)
                    _mac = network.GetPhysicalAddress().ToString();
                }
               
                TerminalInfo info = new TerminalInfo();
                info.MacAddress = _mac;
                info.MachineName = Environment.MachineName;

                IMessage msg = this.CreateMessage(info, message.MessageType);
                this.Send(msg);
            }
        }
    }
}