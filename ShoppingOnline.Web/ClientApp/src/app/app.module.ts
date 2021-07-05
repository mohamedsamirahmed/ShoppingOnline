import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { ProductsComponent } from './products/products.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { CartComponent } from './cart/cart.component';
import { ProductService } from '../Services/product-service';
import { LookupService } from '../Services/lookup-service';
import { AccountService } from '../Services/account-service';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from '../module/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    ProductsComponent,
    ProductDetailsComponent,
    CartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    PaginationModule.forRoot(),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    SharedModule
   
  ],
  providers: [ProductService, LookupService, AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
