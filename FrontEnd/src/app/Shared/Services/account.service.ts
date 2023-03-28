import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";

import { User } from "../Entities/user";
import { environment } from "src/environment/environment";

@Injectable({providedIn: 'root'})

export class AccountService{
        private userSubject: BehaviorSubject<User | null>;
        public user : Observable<User | null>;

        constructor(
            private http: HttpClient,
            private router: Router
        ) {
            this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
            if(this.userSubject.value != null)
                this.userSubject.value.role = this.decodeToken().role;
            this.user = this.userSubject.asObservable();
         }

        public get userValue() {
            return this.userSubject.value;
        } 
        
        login(UserName: string, Password: string) {
            return this.http.post<User>(`${environment.apiUrl}/auth/Authenticate`, {UserName, Password})
                    .pipe(map(user => {
                        localStorage.setItem('user', JSON.stringify(user));
                        user.role = this.decodeToken().role;
                        this.userSubject.next(user);
                        return user;
                    }));
        }

        logout() {
            localStorage.removeItem('user');
            this.userSubject.next(null);
            this.router.navigate(['/account/login']);
        }

        getUser(id: string) {
            return this.http.get<User>(`${environment.apiUrl}/auth/${id}`);
        }

        register(user: User) {
            return this.http.post(`${environment.apiUrl}/auth/register`, user);
        }

        decodeToken(){
            const user = JSON.parse(localStorage.getItem("user") || "");
            const jwtHelper = new JwtHelperService();
            return jwtHelper.decodeToken(user.token);
        }
}