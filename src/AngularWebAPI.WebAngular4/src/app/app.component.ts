import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
}
export class Employee {
    public employeeID: number;
    public firstname: string;
    public lastname: string;
    public dateOfBirth: string;
    public position: string;
    public gender: string;
    public dependants: Dependant[]
}
export class Dependant {
    public employeeID: number;
    public firstname: string;
    public lastname: string;
    public relationship: string;
    public id: number;
    public gender: string;
}
export class EmployeeImage {
    public employeeID: number;
    public ID: number;
    public Image: any;
}
