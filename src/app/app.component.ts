import { Component } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from 'angularfire2/database';
import { AngularFirestoreCollection, AngularFirestore } from 'angularfire2/firestore';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  items: Array<User>;
  selected: User;

  constructor(public aft: AngularFirestore, public af: AngularFireDatabase  ) { 
    this.selected = new User();
    this.LoadData();
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
        let user:User = item.payload.val();
        user.key = item.key;
        this.items.push(user);
      });
    });
    

    /*
    let collection: AngularFirestoreCollection<IPerson> = this.aft.collection('users');
    collection.valueChanges().subscribe(snapshots => {
      snapshots.forEach(item => {
        this.items.push(item);
      });
    });
    */
  }

  Add() {

    if(this.selected.key == null){
      this.af.list("/users").push(this.selected);
    } 
    else
    {
      this.af.list("/users").update(this.selected.key, this.selected);
    }

      /*
    let collection: AngularFirestoreCollection<IPerson> = this.aft.collection('users');
    
    collection.doc(this.selected.key).set(Object.assign({}, this.selected));
    */
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