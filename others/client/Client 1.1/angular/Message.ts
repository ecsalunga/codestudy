export class Message {
    MessageType: string;
    Payload: string;

    public static Parse<T>(payload: string): T {
        return <T>JSON.parse(payload);
    }

    public ToString(): string {
        return JSON.stringify(this);
    }
}