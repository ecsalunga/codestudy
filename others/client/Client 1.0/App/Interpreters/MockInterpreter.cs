using Genting.Infrastructure.CommonServices.Client.Core;
using Genting.Infrastructure.CommonServices.Client.Interpreters.Models;

namespace Genting.Infrastructure.CommonServices.Client.Interpreters
{
    public sealed class MockInterpreter : InterpreterBase
    {
        public MockInterpreter()
        {
            this.InterpreterType = "mock";
            this.Status = ItemState.Default;
        }

        public override void Translate(IMessage message)
        {
            MockInfo mMock = JsonSerializer.Instance.Deserialize<MockInfo>(message.Payload);
            mMock.Message = "mock modified data";
            this.Send(this.CreateMessage(message));
        }
    }
}
