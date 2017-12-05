using CoreScanner;
using Genting.Infrastructure.CommonServices.Client.Core;
using Genting.Infrastructure.CommonServices.Client.Interpreters.Models;
using System;
using System.Xml;

namespace Genting.Infrastructure.CommonServices.Client.Interpreters
{
    public class BarcodeInterpreter : InterpreterBase, ISetting
    {
        public string XMLIn { get; private set; }
        CCoreScanner scanner;

        public BarcodeInterpreter()
        {
            this.InterpreterType = "barcode";
            this.Status = ItemState.Default;
        }

        public void Init()
        {
            scanner = new CCoreScanner();
            short[] scannerTypes = new short[1];
            scannerTypes[0] = 1;
            short numberOfScannerTypes = 1;
            int status;
            int opcode = 1001;
            string outXML;
            string inXML = this.XMLIn;
            scanner.Open(0, scannerTypes, numberOfScannerTypes, out status);
            scanner.ExecCommand(opcode, ref inXML, out outXML, out status);
            scanner.BarcodeEvent += Scanner_BarcodeEvent;
            this.Status = ItemState.Initiated;
        }

        private void Scanner_BarcodeEvent(short eventType, ref string pscanData)
        {
            string data = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pscanData);
            string code = doc.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
            string symbology = doc.DocumentElement.GetElementsByTagName("datatype").Item(0).InnerText;
            string modelnumber = doc.DocumentElement.GetElementsByTagName("modelnumber").Item(0).InnerText;
            string[] items = code.Split(' ');
            foreach (string item in items)
            {
                if (string.IsNullOrEmpty(item))
                    break;

                data += ((char)Convert.ToInt32(item, 16)).ToString();
            }

            BarcodeInfo info = new BarcodeInfo();
            info.Code = data;
            info.ModelNumber = modelnumber.Trim();
            info.SymbologyCode = symbology.Trim();

            IMessage msg = this.CreateMessage(info);
            this.Send(msg);
        }

        public override void Translate(IMessage message)
        {
            if(message.MessageType == "STATUS")
            {
                IMessage msg = this.CreateMessage("[temp] executed", message.MessageType);
                this.Send(msg);
            }
        }

        public void Set(string data)
        {
            this.XMLIn = data;
        }
    }
}