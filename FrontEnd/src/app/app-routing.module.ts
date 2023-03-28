import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./Home/home.component";
import { AuthGuard } from "./Shared/Helpers/auth.guard";


const accountModule = () => import("./Account/account.module").then(x => x.AccountModule);
const userModule = () => import("./User/user.module").then(x => x.UserModule);
const controlPanelModule = () => import("./Control Panel/controlPanel.module").then(x => x.ControlPanelModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'account', loadChildren: accountModule},
    { path: 'user', loadChildren: userModule},
    { path: 'control-panel', loadChildren: controlPanelModule},

    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})


export class AppRoutingModule { }