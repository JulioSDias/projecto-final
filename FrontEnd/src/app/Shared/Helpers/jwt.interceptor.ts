import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { environment } from "src/environment/environment";
import { AccountService } from "../Services/account.service";

@Injectable()

export class JwtInterceptor implements HttpInterceptor {
    
    constructor(
        private accountService: AccountService
    ) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        const user = this.accountService.userValue;

        if(user && user.token && request.url.startsWith(environment.apiUrl)) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${user.token}`
                }
            });
        }
        
        return next.handle(request);
    }
}