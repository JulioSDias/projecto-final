import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { LayoutComponent } from "./layout.component";
import { UserRoutingModule } from "./user-routing.module";
import { CartComponent } from "./cart.component";
import { OrdersComponent } from "./orders.component";
import { ProfileComponent } from "./profile.component";

@NgModule ({
    declarations: [
        LayoutComponent,
        ProfileComponent,
        OrdersComponent,
        CartComponent
    ],
    imports: [
        UserRoutingModule,
        CommonModule,
    ]
})

export class UserModule { }
