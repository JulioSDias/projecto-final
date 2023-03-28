import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { LayoutComponent } from "./layout.component";
import { UsersComponent } from "./users.component";


const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            {path: 'users', component: UsersComponent},
        ]
    }
];

@NgModule({
    imports : [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ControlPanelRoutingModule { } 