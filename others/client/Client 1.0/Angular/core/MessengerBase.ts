import { EventEmitter, transition } from '@angular/core';
import { Observable, Subject } from 'rxjs/Rx';
import { IMessage } from "./IMessage";
import { Message } from "./Message";
import { ClientInfo } from "../models";
import { IMesseger } from "./IMesseger";
import { Interpreter } from "./Interpreter";

export abstract class MessengerBase implements IMesseger {
    private Interpreters: Array<Interpreter>;
    public InterpreterTypes: Array<string>;
    public Channel: Subject<IMessage>;
    public OnMessage: EventEmitter<IMessage>;
    private _loaded: boolean = false;
    public get IsLoaded(): boolean {
        return this._loaded;
    }

    public Init(channel: Subject<IMessage>) {
        this.OnMessage = new EventEmitter<IMessage>();
        this.Interpreters = new Array<Interpreter>();
        this.Channel = channel;
        this.Channel.subscribe(message => {
            if(message.InterpreterType == "ClientManager" && message.MessageType == "CLIENT") {
                let info = this.Parse<ClientInfo>(message.Payload);
                this.SetInterpreters(info.Interpreters);
                this._loaded = true;
            }
            else
                this.OnMessage.emit(message);
        });
    }

    public SetInterpreters(interpreterTypes: Array<string>) {
        this.InterpreterTypes = interpreterTypes;
    }

    public Interpreter({interpreterType, messageType = "default"}: {interpreterType: string, messageType?: string}): Observable<IMessage> {
        if(!this.IsLoaded)
            return this.createError("connection not established", interpreterType, messageType);
        else if(this.InterpreterTypes.some(item => item == interpreterType)) {
            let interpreter: Interpreter;
            if(this.Interpreters.some(inter => inter.InterpreterType == interpreterType && inter.MessageType == messageType))
                interpreter = this.Interpreters.find(inter => inter.InterpreterType == interpreterType && inter.MessageType == messageType);
            else {
                interpreter = new Interpreter();
                interpreter.InterpreterType = interpreterType;
                interpreter.MessageType = messageType;
                interpreter.Init(this);
                this.Interpreters.push(interpreter);
            }

            return interpreter.Data;
        }
        else 
            return this.createError("do not exists", interpreterType, messageType);
    }

    private createError(msg: string, interpreterType: string, messageType?: string): Observable<IMessage> {
        let error = new Observable<IMessage>(observer => {
            let message = new Message();
            message.InterpreterType = interpreterType;
            message.MessageType = messageType;
            message.Payload = msg;
            observer.error(message)
        });

        return error;
    }

    public Parse<T>(payload: string): T {
        return Message.Parse<T>(payload);
    }

    public Send(message: IMessage) {
        this.Channel.next(message);
    }
}