import { Observable } from 'rxjs/Observable';

export interface IInterpreter {
    Name: string;
    Show();
}

export class MockInterpreter implements IInterpreter {
    Name: string;
    Show() {
        alert("test");
    }
}

export class ClientManger {
    client: Observable<string>;
    swipe: Observable<string>;

    hardware: Observable<string>;

    constructor() {
        let count = 0;

        this.hardware = new Observable<string>(observer => {
            setInterval(() => {
                observer.next("hardware: " + count);
            }, 5000);
        });

        this.client = new Observable<string>(observer => {

            this.hardware.subscribe(data => {
                observer.next(data + " client: " + count);
            });
        });

        this.swipe = new Observable<string>(observer => {
            this.hardware.subscribe(data => {
                observer.next(data + " swipe: " + count);
            });
        });

        setInterval(() => {
            count++;
        }, 1000)
    }

    GetInterpreter(name: string): Observable<string> {
        return name == "client" ? this.client : this.swipe;
    }
}
