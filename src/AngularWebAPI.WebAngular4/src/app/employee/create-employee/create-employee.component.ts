import { Component, OnInit } from '@angular/core';
import { FormsModule, FormGroup, FormControl } from '@angular/forms';
import {Employee, Dependant, EmployeeImage } from '../../app.component';
import { EmployeeServiceService } from '../../services/employee-service.service';
@Component({
    selector: 'app-create-employee',
    templateUrl: './create-employee.component.html',
    styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {

    dependants: Dependant[];
    dependant: Dependant;
    employeeId:number = 0;
    showPersonal1:boolean = true;
    showPersonal2:boolean = false;
    showPersonal3:boolean = false;
    employee: Employee;
    employeeImage: EmployeeImage;
    name: string;
    id: number;

    isFormInvalid: boolean = false;
    isFirstnameValid: boolean = false;
    isLastnameInvalid: boolean = false;
    isDOBInvalid: boolean = false;
    isGenderInvalid: boolean = false;
    isPositionInvalid: boolean = false;

    constructor(private employeeservice: EmployeeServiceService) {
    }

    ngOnInit() {
        this.employee = new Employee();
        this.employeeImage = new EmployeeImage();
        this.dependant = new Dependant();
        this.employee.gender = "";
        this.employee.position = "";
    }

    addEmployee = function (tab1Form) {
        
        console.log(tab1Form);
        this.employee = tab1Form;
        if (this.employee.firstname == "") {
            this.isFirstnameValid = true;
        }
        else {
            this.isFirstnameValid = false;
        }
        if (this.employee.lastname == "") {
            this.isLastnameValid = true;
        }
        else {
            this.isLastnameValid = false;
        }
        if (this.employee.dateOfBirth == "") {
            this.isDOBInvalid = true;
        }
        else {
            this.isDOBInvalid = false;
        }
        if (this.employee.gender == "") {
            this.isGenderInvalid = true;
        }
        else {
            this.isGenderInvalid = false;
        }
        if (this.employee.position == ""){
            this.isPositionInvalid = true;
        }
        else {
            this.isPositionInvalid = false;
        }
        if (this.isFirstnameValid || this.isLastnameValid || this.isGenderInvalid || this.isPositionInvalid || this.isDOBInvalid) {
            this.isFormInvalid = true;
            return;
        }

        var body = JSON.stringify(this.employee);
        this.employeeservice.AddEmployee(body).subscribe(response => this.employeeId = response);
        this.showPersonal1 = false;
        this.showPersonal2 = true;
    }

    uploadPicture() {
        this.id = 1;
        //var employeeImage = {
        //    employeeID = "",
        //    Image = ""
        //};
        //var file = document.getElementById("imageFile").files[0];
        //var r = new FileReader();
        //r.onloadend = function (e) {

        //    var arr = Array.from(new Uint8Array(e.target.result));
        //    employeeImage.employeeID = this.employeeId;
        //    employeeImage.Image = arr;
           

        //    $http.post(`${baseUrl}api/EmployeeImage/UploadImage`, EmployeeImage)
        //        .then(
        //        function (response) {
        //            console.log(response);
        //        },

        //        function (reason) {

        //            console.log(reason);
        //        })
        //}
        //r.readAsArrayBuffer(file);
        this.showPersonal2 = false;
        this.showPersonal3 = true;
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

}

