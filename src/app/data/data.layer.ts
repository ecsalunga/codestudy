import { Injectable } from '@angular/core';
import { Core } from './../core';
import { DataAccess, UserDA, UserInfo } from './';

@Injectable()
export class DataLayer {    
    constructor(private core: Core, private DA: DataAccess) {
        
    }
}