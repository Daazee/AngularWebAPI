import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { EmployeeDetailComponent } from "./employee/employee-detail/employee-detail.component";
import {AuthComponent} from './auth/auth.component';
import {OnlyAdminUsers} from './services/role-guard-service';

const routes: Routes = [
    { path: '', redirectTo: '/EmployeeList', pathMatch: 'full' },
    { path: 'EmployeeList', component: EmployeeListComponent,},
    { path: 'AddEmployee', component: CreateEmployeeComponent },
    { path: 'EmployeeDetail/:id', component: EmployeeDetailComponent,canActivate:[OnlyAdminUsers]},
    { path: 'login', component: AuthComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
