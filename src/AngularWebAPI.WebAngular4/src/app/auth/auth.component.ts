import { AuthService } from '../../app/services/auth-service';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { LocalStorageService } from 'angular-2-local-storage';

@Component({
    selector: 'login-view',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})

export class AuthComponent {
    private loginModel: LoginModel;
    private errorMessage: string;
    constructor(private oauthService: OAuthService,
        private authenticationService: AuthService,
        private router: Router,
        private storageService: LocalStorageService
    ) {
        this.loginModel = new LoginModel();
        this.errorMessage = '';
    }

    doSingIn(form: NgForm): void {
        var username: string = this.loginModel.Username;
        var password: string = this.loginModel.Password;
        this.errorMessage = "";
        console.log('form ->', form.value);
        this.authenticationService.login(username, password)
            .then(
            (response) => {
                console.log('login succeeded => ', response);
                var userInfo=this.authenticationService.loadUserInfo();
                if(!userInfo.userInfo)   
                      this.authenticationService.refreshUserInfo();
                this.refreshUserToken(response);
                this.router.navigate(['/EmployeeList']);
            },
            (error) => {
                if (!error || error == null)
                    alert('Oops ! Request could not be completed.')
                switch (error.status) {
                    case 400: {
                        var errorResponse = JSON.parse(error._body);
                        if (errorResponse && errorResponse.error && errorResponse.error == "invalid_grant")
                            this.errorMessage = errorResponse.error_description;
                        break;
                    }
                }
                console.log('Authentication attempt failed => ', error);
                console.log(error._body);
            });
    }

    private refreshUserToken(response): void {
        var accessTokenExist = this.storageService.get('access_token') || false;
        if (accessTokenExist)
            this.storageService.remove("access_token");
        this.storageService.set("access_token", response["access_token"]);
        if(this.storageService.get('userRole'))
            this.storageService.remove('userRole');
        this.storageService.set('userRole',response["Roles"]);
    }
}

export class LoginModel {
    Username: string
    Password: string
}