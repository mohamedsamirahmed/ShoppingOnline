import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { ProductService } from '../Services/product-service';
import { LookupService } from '../Services/lookup-service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AccountService } from '../Services/account-service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    PaginationModule.forRoot(),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot([
      {
        path: '', component: HomeComponent
      },
      { path: 'register', component: RegisterComponent},
      //{ path: 'customerVehicleHistory/:id/:vin/:regNo', component: CustomerVehicleDetailComponent },
      { path: '**', redirectTo: '', pathMatch: 'full' }
    ])
  ],
  providers: [ProductService, LookupService, AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
