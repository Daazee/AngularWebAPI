import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
//import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { EmployeeServiceService } from '../../services/employee-service.service';
import { Employee } from "../../app.component";

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

    public loading = false;

    public employees: Employee[];
    public employee: Employee;
    searchText: any = { firstname: '', lastname: '', position: '' };

    constructor(private _router: Router,
                private _employeeService: EmployeeServiceService,) {
            this._employeeService.getEmployees().subscribe(data => {
            this.employees = data;
            this.employee = data[0];
            console.log(this.employees);
        });        
    }

  ngOnInit() {
  }

  onDeleteEmployee(id: any) {
      console.log(id)
      if (confirm("Are you sure you want to delete this employee?")) {
          this._employeeService.DeleteEmployee(id);
      }
      location.reload();
  }

  gotoDetail(id): void {
      this._router.navigate(['/EmployeeDetail', id]);
  }

  onGetEmployee(id: any) {
      this._employeeService.getEmployee(id).subscribe(data => {
          this.employee = data;
      });
  }

  updateEmployee(id: any) {
      this.loading = true;
      var body = JSON.stringify(this.employee);
      this._employeeService.UpdateEmployee(id, body).subscribe(data => {
          this.employee = data;         
      });      
      location.reload();
      this.loading = false;
  }
}
