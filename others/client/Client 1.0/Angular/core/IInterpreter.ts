import { EventEmitter } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { IMessage } from "./IMessage";
import { IMesseger } from "./IMesseger";

export interface IInterpreter {
    InterpreterType: string;
    MessageType: string;
    Data: Observable<IMessage>;
    Init(messenger: IMesseger) 
}