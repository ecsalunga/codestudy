import { Observable, Subject } from 'rxjs/Rx';
import { Injectable } from "@angular/core";
import { IMessage, IMesseger, WebsocketMessenger  } from "./";

@Injectable()
export class Manager {
    private messenger: IMesseger;
    
    constructor() {
        this.messenger = new WebsocketMessenger();
    }

    public get Interpreters(): Array<string> {
        return this.messenger.InterpreterTypes;
    }

    public Send(message: IMessage) {
        this.messenger.Send(message);
    }

    public Interpreter({interpreterType, messageType}: {interpreterType: string, messageType?: string}): Observable<IMessage> {
        return this.messenger.Interpreter({interpreterType, messageType});
    }
}