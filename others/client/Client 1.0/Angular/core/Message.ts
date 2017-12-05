import { IMessage } from "./IMessage";
import { retry } from "rxjs/operators/retry";

export class Message implements IMessage {
    InterpreterType: string;
    MessageType: string;
    Payload: string;

    public static Parse<T>(payload: string): T {
        return <T>JSON.parse(payload);
    }

    public ToString(): string {
        return JSON.stringify(this);
    }
}