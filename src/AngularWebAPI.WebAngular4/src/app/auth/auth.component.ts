import { AuthService } from '../../app/services/auth-service';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import {Router} from '@angular/router';
import {NgForm} from '@angular/forms';

@Component({
    selector: 'login-view',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})

export class AuthComponent {
    private loginModel: LoginModel;
    private errorMessage:string;
    constructor(private oauthService: OAuthService, 
        private authenticationService: AuthService,
        private router:Router
    ) {
        this.loginModel = new LoginModel();
        this.errorMessage='';
    }

    doSingIn(form:NgForm): void {
        var username: string = this.loginModel.Username;
        var password: string = this.loginModel.Password;
        console.log('form ->',form.value);
        this.authenticationService.login(username, password)
            .then(
            (response) => {
                console.log('login succeeded');
                this.router.navigate(['/EmployeeList']);
            },
            (error) => {
                if(!error || error==null)
                    alert('Oops ! Request could not be completed.')
                switch(error.status){
                    case 400:{
                        var errorResponse=JSON.parse(error._body);
                        if(errorResponse && errorResponse.error && errorResponse.error=="invalid_grant")
                            this.errorMessage=errorResponse.error_description;
                        break;
                    }
                }
                console.log('Authentication attempt failed => ',error);
                console.log(error._body);
            });
    }
}

export class LoginModel {
    Username: string
    Password: string
}