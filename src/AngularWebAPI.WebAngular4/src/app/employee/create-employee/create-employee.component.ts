import { Component, OnInit } from '@angular/core';
import {Employee, Dependant} from '../../app.component';
import { EmployeeServiceService } from '../../services/employee-service.service';
@Component({
    selector: 'app-create-employee',
    templateUrl: './create-employee.component.html',
    styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {
    model = this;
    dependants:any = [];
    employeeId:number = 0;
    showPersonal1:boolean = true;
    showPersonal2:boolean = false;
    showPersonal3:boolean = false;
    employee:Employee;
    name: string;
    constructor(private employeeservice: EmployeeServiceService) {
    }

    ngOnInit() {

    }

    addEmployee() {
        console.log(this.employee.firstname);
        var body = JSON.stringify(this.employee);
        this.employeeservice.AddEmployee(body).subscribe(response => this.employeeId = response);
        this.showPersonal1 = false;
        this.showPersonal2 = true;
    }

    uploadPicture() {

        this.showPersonal2 = false;
        this.showPersonal3 = true;
    }


    addDependant() {

    }

}

