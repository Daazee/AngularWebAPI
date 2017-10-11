import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { FormsModule, FormGroup, FormControl, NgForm } from '@angular/forms';
import {Employee, Dependant, EmployeeImage } from '../../app.component';
import { EmployeeServiceService } from '../../services/employee-service.service';
import { AppValidationMessages } from "../../app.messages";

@Component({
    selector: 'app-create-employee',
    templateUrl: './create-employee.component.html',
    styleUrls: ['./create-employee.component.css']
})


export class CreateEmployeeComponent implements OnInit {
    
    dependants: Dependant[];
    dependant: Dependant;
    employeeId: number = 0;
    showPersonal1: boolean = true;
    showPersonal2: boolean = false;
    showPersonal3: boolean = false;
    employee: Employee;
    employeeImage: EmployeeImage;
    name: string;
    id: number;
    @ViewChild('fileInput') fileInput;
    @ViewChild('tab1Form') currentForm: NgForm;
    tab1Form: NgForm;

    ngAfterViewChecked() {
        this.formChange();
    }

    formErrors = {
        'firstname': '',
        'lastname': '',
        'dateOfBirth': '',
        'gender': '',
        'position': '',
    };

    formChange() {
        if (this.currentForm === this.tab1Form) { return; }

        this.tab1Form = this.currentForm;
        if (this.tab1Form) {
            this.tab1Form.valueChanges
                .subscribe(data => this.onValueChanged());
        }
    }

    onValueChanged() {
        if (!this.tab1Form) { return; }
        const form = this.tab1Form.form;

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

    constructor(private employeeservice: EmployeeServiceService) {
    }

    ngOnInit() {
        this.employee = new Employee();
        this.employeeImage = new EmployeeImage();
        this.dependant = new Dependant();
        this.employee.gender = "";
        this.employee.position = "";
    }

    
    

    addEmployee () {

        console.log(this.employee);

        var body = JSON.stringify(this.employee);
        this.employeeservice.AddEmployee(body).subscribe(response => this.employeeId = response);
        this.showPersonal1 = false;
        this.showPersonal2 = true;
    }

    addDependant() {

        this.dependant.employeeID = this.employeeId;
        var body = JSON.stringify(this.dependant);
        this.employeeservice.AddEmployeeDependency(body).subscribe(response => {
            this.dependant = response;
            this.employeeservice.getEmployeeDependants(this.employeeId).subscribe(
                data => this.dependants = data);

        });


    }

    uploadPicture(e) {
        var input: any = document.getElementById("imageFile");
        let fileBrowser = this.fileInput.nativeElement;
        if (fileBrowser.files && fileBrowser.files[0]) {
            var r = new FileReader();
            r.onloadend = this._handleReaderLoaded.bind(this);
            r.readAsArrayBuffer(fileBrowser.files[0]);
            this.showPersonal2 = false;
            this.showPersonal3 = true;
        }
    }

    _handleReaderLoaded(e) {
        var a: any = e.target;
        var r = a.result;
        var ar = new Uint8Array(r);
        var arr = Array.from(ar);
        console.log(arr);
        this.employeeImage.Image = arr;
        this.employeeImage.employeeID = this.employeeId;
        var body = JSON.stringify(this.employeeImage);
        this.employeeservice.UploadEmployeePicture(body).subscribe(response => this.employeeImage = response);
    }


}

