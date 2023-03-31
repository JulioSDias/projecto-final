import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { LayoutComponent } from "./layout.component";
import { ProductsComponent } from "./products.component";
import { UsersComponent } from "./users.component";


const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            {path: 'users', component: UsersComponent},
            {path: 'products', component: ProductsComponent}
        ]
    }
];

@NgModule({
    imports : [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ControlPanelRoutingModule { } 