import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeServiceService } from './services/employee-service.service';
import {LocalStorageModule} from 'angular-2-local-storage';
import {AuthService} from './services/auth-service';
import {OnlyAdminUsers} from './services/role-guard-service';
import { HttpinterceptorService,HttpResponseInterceptor } from './service/httpinterceptor.service';
import {HttpModule} from '@angular/http';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { Ng2FilterPipeModule } from 'ng2-filter-pipe';
import {OAuthModule} from 'angular-oauth2-oidc';
import { AppComponent } from './app.component';
import { ContainerComponent } from './container/container.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { EmployeeDetailComponent } from './employee/employee-detail/employee-detail.component';
import {AuthComponent} from './auth/auth.component';
import {UsersComponent} from './users/users.component'


//decorator
@NgModule({
    declarations: [ //declaring all the components
        AppComponent,
        ContainerComponent,
        EmployeeListComponent,
        CreateEmployeeComponent,
        EmployeeDetailComponent,
        AuthComponent,
        UsersComponent
    ],
    imports: [ //importing modules
        BrowserModule,
        HttpClientModule,
        HttpModule,
        AppRoutingModule,
        FormsModule,
        Ng2FilterPipeModule,
        OAuthModule.forRoot(),
        LocalStorageModule.withConfig({
            storageType:'localStorage',
            prefix:'employeeApp'
        })
    ],
    providers: [{ //provide services to all modules' component
        provide: HTTP_INTERCEPTORS,
        useClass: HttpinterceptorService,
        multi: true,
    },
    {
        provide:HTTP_INTERCEPTORS,
        useClass:HttpResponseInterceptor,
        multi:true
    },
     EmployeeServiceService,
     AuthService,
     OnlyAdminUsers
    ],
    bootstrap: [AppComponent]
})


export class AppModule { }
