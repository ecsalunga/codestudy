import { EventEmitter } from '@angular/core';
import { Observable, Subject } from 'rxjs/Rx';
import { IMessage } from "./IMessage";

export interface IMesseger {
    InterpreterTypes: Array<string>;
    Channel: Subject<IMessage>;
    OnMessage: EventEmitter<IMessage>;
    IsLoaded: boolean;
    Send(message: IMessage);
    Interpreter({interpreterType, messageType}: {interpreterType: string, messageType?: string}): Observable<IMessage>;
}