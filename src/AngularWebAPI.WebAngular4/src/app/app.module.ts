import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeServiceService } from './services/employee-service.service';
import { HttpinterceptorService } from './service/httpinterceptor.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { Ng2FilterPipeModule } from 'ng2-filter-pipe';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
//import { HttpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { ContainerComponent } from './container/container.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { EmployeeDetailComponent } from './employee/employee-detail/employee-detail.component';

//decorator
@NgModule({
    declarations: [ //declaring all the components
        AppComponent,
        ContainerComponent,
        EmployeeListComponent,
        CreateEmployeeComponent,
        EmployeeDetailComponent
    ],
    imports: [ //importing modules
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        FormsModule,
        Ng2FilterPipeModule,
        LoadingModule,
        LoadingModule.forRoot({
            animationType: ANIMATION_TYPES.wanderingCubes,
            backdropBackgroundColour: 'rgba(0,0,0,0.1)',
            backdropBorderRadius: '4px',
            primaryColour: '#ffffff',
            secondaryColour: '#ffffff',
            tertiaryColour: '#ffffff'
        })
    ],
    providers: [{ //provide services to all modules' component
        provide: HTTP_INTERCEPTORS,
        useClass: HttpinterceptorService,
        multi: true,
    }, EmployeeServiceService],
    bootstrap: [AppComponent]
})


export class AppModule { }
