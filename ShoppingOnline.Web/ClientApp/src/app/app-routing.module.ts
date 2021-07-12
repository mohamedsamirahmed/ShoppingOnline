import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../guards/auth.guard";
import { AdminGuard } from "../guards/admin.guard";
import { CartComponent } from "./cart/cart.component";
import { HomeComponent } from "./home/home.component";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ProductsComponent } from "./products/products.component";
import { AdminPanelComponent } from "./admin/admin-panel/admin-panel.component";
import { OrderComponent } from "./order/order.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'cart', component: CartComponent },
      { path: 'products', component: ProductsComponent},
      { path: 'products/:id', component: ProductDetailsComponent },
      { path: 'myorders', component: OrderComponent },
      { path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard] }
    ]
  },
  { path: '**', component: HomeComponent, pathMatch: "full" }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports:[RouterModule]
})
export class AppRoutingModule { }


