import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Employee, Dependant } from "../app.component";

@Injectable()
export class EmployeeServiceService {
     response: any;
     public baseUrl: any = 'http://localhost:18558/';
    constructor(private http: HttpClient) { }
    getEmployee(id: any): Observable<Employee>  {

        return this.http.get(this.baseUrl+'api/Employee/'+id).map(data => {
          // Read the result field from the JSON response.
            return data;
        });
    }

    getEmployees(): Observable<Employee[]>  {
        
       return this.http.get(this.baseUrl + 'api/Employee').map(data => {
            // Read the result field from the JSON response.
            return data;
            
        });  
    }

    getEmployeeDependants(id: any): Observable<Dependant[]> {
        return this.http.get(this.baseUrl + '/api/EmployeeDependant/GetDependantsByEmployeeID/'+id)
            .map(response => {
                return response;
            });
    }

    getEmployeeImage(id: any): Observable<any> {
        return this.http.get(this.baseUrl+'/api/EmployeeImage/GETImageByEmployeeID/'+id)
            .map(response => {
                console.log("employee image")
                return response;
            });
    }

    UpdateEmployee(id: any, body: any): Observable<Employee>{

        return this.http.put(this.baseUrl + 'api/Employee/UpdateEmployee/' + id, body).map(response => {
            return response;
        });
    }

    DeleteEmployee(id: any) {

        this.http.delete(this.baseUrl + 'api/Employee/UpdateEmployee/' + id).subscribe(response => {
            return response;
        });
    }

    AddEmployee(body:any): Observable<any> {

       return this.http.post(this.baseUrl + 'api/Employee/AddEmployee/', body).map(response => {
            return response;
        });
    }

    UploadEmployeePicture(body: any) {
        this.http.post(this.baseUrl + 'api/EmployeeImage/UploadImage', body).subscribe(response => {
            return response;
        });
    }

    AddEmployeeDependency(body: any) {
        this.http.post(this.baseUrl + 'api/EmployeeDependant/AddDependant', body).subscribe(response => {
            return response;
        });
    }
}
