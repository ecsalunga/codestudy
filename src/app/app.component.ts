import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from 'angularfire2/database';
import { Subscription } from 'rxjs/Subscription';
import { ClientManger } from "./data/models/IInterpreter";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('ViewContainer', {read: ViewContainerRef})
  ViewContainer: ViewContainerRef;

  title = 'app';
  items: Array<User>;
  selected: User;
  cm: ClientManger;
  CurrentNumber: string;
  subs: Subscription;
  constructor( public af: AngularFireDatabase ) { 
    this.selected = new User();
    this.LoadData();
    this.cm = new ClientManger();
  }

  Test() {
    this.subs = this.cm.GetInterpreter("client").subscribe(num => {
      this.CurrentNumber = num;
    });
  }

  Test2() {
    this.subs = this.cm.GetInterpreter("swipe").subscribe(num => {
      this.CurrentNumber = num;
    });
  }

  Stop() {
    this.subs.unsubscribe();
  }

  Select(item: User) {
    this.selected = item;
  }

  LoadData() {
    this.items = new Array<User>();
    this.af.list<User>("/users", ref => {
      return ref.orderByChild('Email');
    }).snapshotChanges().subscribe(snpashots => {
      snpashots.forEach(item => {
        let user: User = item.payload.val();
        user.key = item.key;
        this.items.push(user);
      });
    });
  }

  Add() {

    if(this.selected.key == null){
      this.af.list("/users").push(this.selected);
    } 
    else
    {
      this.af.list("/users").update(this.selected.key, this.selected);
    }
  }
}

export class User implements IPerson {
  key: string;
  Name: string;
  Email: string;

  constructor() {
    this.Name = '';
    this.Email = '';
  }
}

interface IPerson {
  key: string;
  Name: string;
  Email: string
}