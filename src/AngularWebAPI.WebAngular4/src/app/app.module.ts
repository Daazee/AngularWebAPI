import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ContainerComponent } from './container/container.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { EmployeeServiceService } from './services/employee-service.service';
import { HttpinterceptorService } from './service/httpinterceptor.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//import { HttpModule } from '@angular/http';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { EmployeeDetailComponent } from './employee/employee-detail/employee-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Ng2FilterPipeModule } from 'ng2-filter-pipe';

@NgModule({
    declarations: [
        AppComponent,
        ContainerComponent,
        EmployeeListComponent,
        CreateEmployeeComponent,
        EmployeeDetailComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        FormsModule,
        Ng2FilterPipeModule
    ],
    providers: [{
        provide: HTTP_INTERCEPTORS,
        useClass: HttpinterceptorService,
        multi: true,
    }, EmployeeServiceService],
    bootstrap: [AppComponent]
})


export class AppModule { }
