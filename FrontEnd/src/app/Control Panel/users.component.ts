import { Component, OnInit } from "@angular/core";
import { User } from "../Shared/Entities/user";
import { ControlPanelService } from "../Shared/Services/controlPanel.service";

@Component({
    templateUrl: "users.component.html"
})

export class UsersComponent { 
    public isCollapsed: Array<boolean> = [];
    icon_val = "bi bi-chevron-compact-down";
    users?: Array<User>;
    
    constructor(
        private controlPanelService: ControlPanelService
    ) { 
        this.getTable();
    }
    getTable() {
        this.controlPanelService.getAllUsers()
            .subscribe({
                next: (data) => { 
                    this.users = data;
                }
            })
    }
    teste(){
        console.log(this.isCollapsed);
    }

}