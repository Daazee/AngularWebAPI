import { Injectable } from '@angular/core';
import {OAuthService} from 'angular-oauth2-oidc';
import {Router} from '@angular/router';
import {HttpErrorResponse} from '@angular/common/http';
import {ReflectiveInjector} from '@angular/core';

import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
@Injectable()
export class HttpinterceptorService implements HttpInterceptor {

    constructor(private oauthService:OAuthService){

    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const changedReq = req.clone({
            headers: req.headers.set('Content-Type', 'application/json')
                                .set('Authorization',`Bearer ${this.oauthService.getAccessToken()}`)
        });
        return next.handle(changedReq);
    }
}
@Injectable()
export class HttpResponseInterceptor implements HttpInterceptor{
    constructor(private router:Router){
    }
    intercept(request:HttpRequest<any>,next:HttpHandler){
        return next.handle(request).do(
            (event:HttpEvent<any>)=>{
                // do nothing on success
            },
            (err:any)=>{
                if(err instanceof HttpErrorResponse){
                    if(err.status===401)
                        this.router.navigate(['/login']);
                }
            }
        );
    }
}