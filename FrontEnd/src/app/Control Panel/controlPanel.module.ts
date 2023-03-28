import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { LayoutComponent } from "./layout.component";
import { ControlPanelRoutingModule } from "./controlPanel-rounting.module";
import { UsersComponent } from "./users.component";
import { NgbCollapseModule } from "@ng-bootstrap/ng-bootstrap";


@NgModule ({
    declarations: [
        LayoutComponent,
        UsersComponent
    ],
    imports: [
        ControlPanelRoutingModule,
        CommonModule,
        NgbCollapseModule,
    ]
})

export class ControlPanelModule { }