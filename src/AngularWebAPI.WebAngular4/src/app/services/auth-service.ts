import {Injectable} from '@angular/core';
import {OAuthService} from 'angular-oauth2-oidc';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthService{

    constructor(private oauthService:OAuthService){

    }

    login(username:string,password:string):Promise<object>{
        var response= this.oauthService.fetchTokenUsingPasswordFlow(username,password);
        return response;
    }
    
}