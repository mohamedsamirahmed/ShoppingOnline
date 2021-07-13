import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { ProductCardComponent } from './product-card/product-card.component';
import { JwtInterceptor } from '../interceptors/jwt.interceptor';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from '../directives/has-role.directive';
import { OrderDeliveryModalsComponent } from '../modals/order-delivery-modals/order-delivery-modals.component';
import { OrderManagementComponent } from './admin/order-management/order-management.component';
import { OrderShipmentaddressModalComponent } from '../modals/order-shipmentaddress-modal/order-shipmentaddress-modal.component';
import { OrderComponent } from './order/order.component';
import { OrderDetailsComponent } from './order-details/order-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    ProductsComponent,
    ProductDetailsComponent,
    CartComponent,
    ProductCardComponent,
    AdminPanelComponent,
    HasRoleDirective,
    OrderDeliveryModalsComponent,
    OrderManagementComponent,
    OrderShipmentaddressModalComponent,
    OrderComponent,
    OrderDetailsComponent
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
  entryComponents: [
    OrderShipmentaddressModalComponent,
    OrderDeliveryModalsComponent
  ],
  providers: [ProductService, LookupService, AccountService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor , multi:true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
