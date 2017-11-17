import { Injectable, ComponentFactory, ComponentFactoryResolver, ViewContainerRef } from '@angular/core';

@Injectable()
export class Core {
    public ViewContainer: ViewContainerRef;
    private components: Array<any> = new Array<any>();
    public selector: string;

    constructor(private resolver: ComponentFactoryResolver) { }

    public LoadComponent(selector: string) {
        let factories = Array.from(this.resolver['_factories'].values());
        let component = <ComponentFactory<any>> factories.find((item: ComponentFactory<any>) => item.selector === selector);
        if(component) {
            this.clearComponent();
            this.selector = selector;
            let factory = this.resolver.resolveComponentFactory(component.componentType);
            let created = this.ViewContainer.createComponent(factory);
            this.components.push(created);
        }
    }

    public LoadComponents(selectors: Array<string>) {
        this.selector = "Multiple";
        selectors.forEach(selector => {
            let factories = Array.from(this.resolver['_factories'].values());
            let component = <ComponentFactory<any>> factories.find((item: ComponentFactory<any>) => item.selector === selector);
            if(component) {
                let factory = this.resolver.resolveComponentFactory(component.componentType);
                let created = this.ViewContainer.createComponent(factory);
                this.components.push(created);
            }
        });
    }

    public DateToNumber(date: Date): number
    {
        let dt: Date = new Date();
        let strDate = date.getFullYear() + this.az(date.getMonth()+1) + this.az(date.getDate()) + this.az(dt.getHours()) + this.az(dt.getMinutes()) + this.az(dt.getSeconds());
        return parseInt(strDate);
    }

    public DateToKeyDay(date: Date): number
    {
        let strDate = date.getFullYear() + this.az(date.getMonth()+1) + this.az(date.getDate());
        return parseInt(strDate);
    }

    public DateToKeyMonth(date: Date): number
    {
        let strDate = date.getFullYear() + this.az(date.getMonth()+1);
        return parseInt(strDate);
    }

    public NumberToDate(num: number): Date
    {
        let val = num.toString();
        let year = parseInt(val.substring(0, 4));
        let month = parseInt(val.substring(4, 6));
        let day = parseInt(val.substring(6, 8));
        let hour = parseInt(val.substring(8, 10));
        let minute = parseInt(val.substring(10, 12));
        let second = parseInt(val.substring(12, 14));
        
        return new Date(year, month-1, day, hour, minute, second);
    }

    public KeyDayToDate(keyDay: number): Date
    {
        return this.NumberToDate(parseInt(keyDay + '000000'));
    }

    private clearComponent() {
        this.selector = "";
        this.components.forEach(component => {
            component.destroy();
        });
    }

    private az(val: number) : string {
        let num = val.toString();
        if(num.length < 2)
            num = "0" + num;
        
        return num;
    }
}