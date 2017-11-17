import { Injectable, EventEmitter } from '@angular/core';

import * as firebase from 'firebase/app';
import { AngularFireAuth } from 'angularfire2/auth';
import { AngularFireDatabase } from 'angularfire2/database';

import { Core } from './../core';
import { UserDA, UserInfo } from './';

@Injectable()
export class DataAccess {
    DataLoaded: EventEmitter<string> = new EventEmitter();
    FileUploaded: EventEmitter<string> = new EventEmitter();

    private UserDA: UserDA;
    private fbS: firebase.storage.Reference = firebase.storage().ref();

    constructor(private core: Core, private fbD: AngularFireDatabase, private fbA: AngularFireAuth) {
        
    }
}