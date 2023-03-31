import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { NgbCollapseModule } from "@ng-bootstrap/ng-bootstrap";

import { LayoutComponent } from "./layout.component";
import { ControlPanelRoutingModule } from "./controlPanel-rounting.module";
import { UsersComponent } from "./users.component";
import { ProductsComponent } from "./products.component";


@NgModule ({
    declarations: [
        LayoutComponent,
        UsersComponent,
        ProductsComponent
    ],
    imports: [
        ControlPanelRoutingModule,
        CommonModule,
        NgbCollapseModule,
        ReactiveFormsModule
    ]
})

export class ControlPanelModule { }