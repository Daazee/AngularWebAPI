import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ContainerComponent } from './container/container.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { EmployeeServiceService } from './services/employee-service.service';
import { HttpinterceptorService } from './service/httpinterceptor.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { AppRoutingModule } from './app-routing.module';
import { EmployeeDetailComponent } from './employee/employee-detail/employee-detail.component';


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
  ],
providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpinterceptorService,
    multi: true,
}, EmployeeServiceService],
  bootstrap: [AppComponent]
})

export class AppModule { }
