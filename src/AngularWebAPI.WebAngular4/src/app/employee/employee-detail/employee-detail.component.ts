import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import { EmployeeServiceService } from '../../services/employee-service.service';
import { Employee, Dependant } from "../../app.component";

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

    public id: any;
    public employee: Employee;
    public dependants: Dependant[];
    public image: any;
    constructor(private _route: ActivatedRoute, private _router: Router, private _employeeService: EmployeeServiceService) {
        this._route.params.subscribe(params => this.id = params["id"])
        this._employeeService.getEmployee(this.id).subscribe(data => {
            this.employee = data;
        });
        this._employeeService.getEmployeeDependants(this.id).subscribe(data => {
            this.dependants = data;
        });
        this._employeeService.getEmployeeImage(this.id).subscribe(data => {
            this.image = data;
        });
    }

  ngOnInit() {
  }

}
