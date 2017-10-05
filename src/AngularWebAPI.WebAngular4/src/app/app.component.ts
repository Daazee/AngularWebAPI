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
    public employeeID: string;
    public firstname: string;
    public lastname: string;
    public dateOfBirth: string;
    public position: string;
    public gender: string;
    public dependants: Dependant[]
}
export class Dependant {
    public employeeID: string;
    public firstname: string;
    public lastname: string;
    public relationship: string;
    public id: string;
    public gender: string;
}
export class EmployeeImage {
    public employeeID: string;
    public ID: string;
    public Image: any;
}
