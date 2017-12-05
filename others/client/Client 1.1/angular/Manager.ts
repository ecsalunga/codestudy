import { Injectable, EventEmitter } from '@angular/core';
import { Observable, Subject, Observer } from 'rxjs/Rx';
import { Message } from "./Message";
import { Subscriber } from 'rxjs/Subscriber';

@Injectable()
export class Manager {
    public OnMessage: Observable<Message>;
    private websocket: WebSocket;
    private status: string;
    private observer: Subscriber<Message>;

    constructor() {
        this.connect();
        this.OnMessage = new Observable(obs => { this.observer = obs; });
    }

    private connect() {
        if(this.status != "connecting") {
            this.status = "connecting";
            this.websocket = this.create("ws://127.0.0.1:8031/service");
        }
    }

    private reconnect() {
        if(this.status != "reconnecting") {
            this.status = "reconnecting";
            this.websocket = null;
            console.log("disconnected, reconnecting...");
            setTimeout(this.connect.bind(this), 5000);
        }
    }

    private wsClose() {
        this.reconnect();
    }

    private wsError() {
        this.reconnect();
    }

    private wsOpen() {
        this.status = "open";
        console.log("connection established.");
    }

    private wsMessage(response: MessageEvent) {
        this.status = "receive";
        this.observer.next(Message.Parse<Message>(response.data));
    }

    public Send(message: Message) {
        this.status = "sending";
        this.websocket.send(JSON.stringify(message));
    }

    private create(path: string): WebSocket {
        let ws = new WebSocket(path);
        ws.onmessage = this.wsMessage.bind(this);
        ws.onclose = this.wsClose.bind(this);
        ws.onerror = this.wsError.bind(this);
        ws.onopen = this.wsOpen.bind(this);
        return ws;
    }
}