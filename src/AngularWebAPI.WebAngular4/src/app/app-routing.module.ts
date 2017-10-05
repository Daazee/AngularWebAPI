import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { EmployeeDetailComponent } from "./employee/employee-detail/employee-detail.component";

const routes: Routes = [
    { path: '', redirectTo: '/EmployeeList', pathMatch: 'full' },
    { path: 'EmployeeList', component: EmployeeListComponent },
    { path: 'AddEmployee', component: CreateEmployeeComponent },
    { path: 'EmployeeDetail/:id', component: EmployeeDetailComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
