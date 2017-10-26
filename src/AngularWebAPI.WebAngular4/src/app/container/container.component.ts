import { Component, OnInit } from '@angular/core';
import {AuthService} from '../services/auth-service';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent implements OnInit {

  private loggedInUser:string
  constructor(private authenticationService:AuthService) { }

  ngOnInit() {
    var userData=this.authenticationService.loadUserInfo();
    var appUserInfo=userData.userInfo;
    if(appUserInfo)
      this.loggedInUser=appUserInfo["email"];
  }

  logOutUser():void{
    this.authenticationService.logOut();
  }

}
