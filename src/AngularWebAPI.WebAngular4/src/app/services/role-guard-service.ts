import {CanActivate} from '@angular/router';
import {AuthService} from './auth-service';
import {LocalStorageService} from 'angular-2-local-storage';
import {ActivatedRouteSnapshot} from '@angular/router';
import {RouterStateSnapshot} from '@angular/router';
import {Injectable} from '@angular/core';

@Injectable()
export class OnlyAdminUsers implements CanActivate{

    constructor(private authenticationService:AuthService){

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean  | Promise<boolean> {
        //throw new Error("Method not implemented.");
        var userData=this.authenticationService.loadUserInfo();
        if(!userData || !userData.userRole){
            alert('You donot have sufficient access right to view this page');
            return false;
        }
        if(userData.userRole==="SuperAdmin")
            return true;
        alert('You donot have sufficient access right to view this page');
        return false;

    }

}