import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
//import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { EmployeeServiceService } from '../../services/employee-service.service';
import { Employee } from "../../app.component";
import { AppValidationMessages } from "../../app.messages";

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {   

    //Properties

    public employees: Employee[];
    public employee: Employee;
    searchText: any = { firstname: '', lastname: '', position: '' };

    @ViewChild('myForm') currentForm: NgForm;
    myForm: NgForm;

    formErrors = {
        'firstname': '',
        'lastname': '',
        'gender': '',
        'position': ''
    };

    //Properties

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

    ngAfterViewChecked() {
        this.formChange();
    }

    //ValidationEvents

    formChange() {
        if (this.currentForm === this.myForm) { return; }

        this.myForm = this.currentForm;
        if (this.myForm) {
            this.myForm.valueChanges
                .subscribe(data => this.onValueChanged());
        }
    }

    onValueChanged() {
        if (!this.myForm) { return; }
        const form = this.myForm.form;

        for (const field in this.formErrors) {
            this.formErrors[field] = '';
            const control = form.get(field);

            if (control && control.dirty && !control.valid) {
                const messages = AppValidationMessages.errorMessages[field];
                for (const key in control.errors) {
                    this.formErrors[field] = messages[key];
                }
            }
        }
    }

    //ValidationEvents

    //Methods

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
        console.log(this.employee);
        var body = JSON.stringify(this.employee);
        this._employeeService.UpdateEmployee(id, body).subscribe(data => {
            this.employee = data;
        });
        location.reload();
    }

    //Methods
}
