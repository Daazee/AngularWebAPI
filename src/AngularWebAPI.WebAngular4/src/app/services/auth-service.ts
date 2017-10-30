import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { LocalStorageService } from 'angular-2-local-storage';
import { HttpClient} from '@angular/common/http'

import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthService {

    constructor(private oauthService: OAuthService, private storageService: LocalStorageService, private http:HttpClient) {

    }
    login(username: string, password: string): Promise<object> {
        var response = this.oauthService.fetchTokenUsingPasswordFlow(username, password);
        return response;
    }
    refreshUserInfo() {
        let userInformation = this.oauthService.loadUserProfile()
            .then(
            (response) => {
                var userInfoExist = this.storageService.get('userInfo') || false;
                if (userInfoExist)
                    this.storageService.remove('userInfo');
                this.storageService.set("userInfo", response);
            },
            (error) => {
                console.log('error occured on loading => ', error);
            });
    }

    loadUserInfo() {
        let userInfo = this.storageService.get('userInfo');
        let userRole = this.storageService.get('userRole');
        return {
            userInfo: userInfo,
            userRole: userRole
        };
    }

    loadUsers():Promise<object>{
        let usersListApiUrl="http://localhost:18558/api/accounts";
        return this.http.get(usersListApiUrl).toPromise<object>();
    }

    logOut(){
        this.storageService.clearAll();
        this.oauthService.logOut();
    }
}