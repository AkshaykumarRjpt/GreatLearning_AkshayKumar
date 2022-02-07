import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { User } from '../../_models/User';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')??'{}'));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    getUsername(): string
    {
       return this.currentUserValue.firstName + " " + this.currentUserValue.lastName
    }

    login(email: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/login`, { email, password })
            .pipe(map(user => {
                if(user == null) {
                    return null;
                } 
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user.currentUser));
                this.setToken(JSON.stringify(user.token));
                this.currentUserSubject.next(user.currentUser);
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        localStorage.removeItem('token');
    }

    setToken(value: string)
    {
        localStorage.setItem('token', value);
    }

    public getToken()
    {
        return localStorage.getItem('token');
    }

    public isLoggedIn() {
        if(this.getToken()) {
            return true;
        }
        return false;
    }
}