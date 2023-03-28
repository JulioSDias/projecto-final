import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule} from "@angular/forms";

import { LayoutComponent } from "./layout.component";
import { AccountRoutingModule } from "./account-routing.module";
import { LoginComponent } from "./login.component";
import { RegisterComponent } from "./register.component";

@NgModule ({
    declarations: [
        LayoutComponent,
        LoginComponent,
        RegisterComponent,
    ],
    imports: [
        AccountRoutingModule,
        CommonModule,
        ReactiveFormsModule,
    ]
})

export class AccountModule { }