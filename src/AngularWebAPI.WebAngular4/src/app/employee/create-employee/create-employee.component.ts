import { Component, OnInit } from '@angular/core';
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

    constructor(private employeeservice: EmployeeServiceService) {
    }

    ngOnInit() {
        this.employee = new Employee();
        this.employeeImage = new EmployeeImage();
        this.dependant = new Dependant();        
    }

    addEmployee() {
        console.log(this.employee.firstname);
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

