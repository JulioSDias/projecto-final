import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CartComponent } from "./cart.component";

import { LayoutComponent } from "./layout.component";
import { OrdersComponent } from "./orders.component";
import { ProfileComponent } from "./profile.component";


const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            {path: 'profile', component: ProfileComponent},
            {path: 'orders', component: OrdersComponent},
            {path: 'cart', component: CartComponent},
        ]
    }
];

@NgModule({
    imports : [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UserRoutingModule { } 