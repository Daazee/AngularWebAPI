import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ContainerComponent } from './container/container.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { EmployeeServiceService } from './services/employee-service.service';
import { HttpClientModule } from '@angular/common/http';
//import { HttpModule } from '@angular/http';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';

import { AppRoutingModule } from './app-routing.module';


@NgModule({
  declarations: [
    AppComponent,
    ContainerComponent,
    EmployeeListComponent,
    CreateEmployeeComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
  ],
  providers: [EmployeeServiceService],
  bootstrap: [AppComponent]
})


export class AppModule { }
