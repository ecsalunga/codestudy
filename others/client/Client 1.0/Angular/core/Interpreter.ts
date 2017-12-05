import { Observable } from "rxjs/Observable";
import { IInterpreter } from "./IInterpreter";
import { IMessage } from "./IMessage";
import { IMesseger } from "./IMesseger";

export class Interpreter implements IInterpreter  {
    InterpreterType: string;
    MessageType: string = "default";
    Data: Observable<IMessage>;

    Init(messenger: IMesseger) {
        this.Data = new Observable<IMessage>(observer => {
            messenger.OnMessage.subscribe((data: IMessage) => {
                if(data.InterpreterType == this.InterpreterType && (this.MessageType == "default" || data.MessageType == this.MessageType))
                    observer.next(data);
            });
        });
    }
}