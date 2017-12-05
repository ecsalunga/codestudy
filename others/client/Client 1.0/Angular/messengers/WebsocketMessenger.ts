import { Observable, Subject, Observer } from 'rxjs/Rx';
import { IMessage, Message, MessengerBase } from "../core";

export class WebsocketMessenger extends MessengerBase {
    constructor() {
        super();

        let websocket = <Subject<IMessage>>this.create("ws://127.0.0.1:8031/service")
        .map((response: MessageEvent): IMessage => {
            return this.Parse<IMessage>(response.data);
        });
        super.Init(websocket);
    }

    private create(path: string): Subject<MessageEvent> {
        let ws = new WebSocket(path);
        
        let observable = Observable.create((obs: Observer<MessageEvent>) => {
            ws.onmessage = obs.next.bind(obs);
            ws.onerror = obs.error.bind(obs);
            ws.onclose = obs.complete.bind(obs);
            return ws.close.bind(ws);
        });

        let observer = {
            next: (data: IMessage) => {
                if (ws.readyState === WebSocket.OPEN)
                    ws.send(JSON.stringify(data));
            }
        }
    
        return Subject.create(observer, observable);
    }
}