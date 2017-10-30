import { AuthService } from '../services/auth-service';
import { Component, OnInit } from '@angular/core';


@Component({
    selector: 'users-view',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

    private usersList;
    constructor(private authenticationService: AuthService) {
        this.usersList = [];
    }

    ngOnInit() {
        this.loadUsers();
    }

    loadUsers(): void {
        this.authenticationService.loadUsers().then(
            (response) => {
                console.log('users list response -> ', response);
                this.usersList = response;
            },
            (error) => {
                console.log('error occured while loading users list -> ', error);
            }
        );
    }

    getUserTypeString(userType: number): string {
        switch (userType) {
            case 1:
                return "User";
            case 2:
                return "Admin";
            case 3: return "Super Admin";
            default: return "N/A";
        }
    }




}