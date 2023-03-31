import { Component, OnInit} from "@angular/core";
import { FormBuilder, FormGroup, Validators} from "@angular/forms";

import { User } from "../Shared/Entities/user";
import { ControlPanelService } from "../Shared/Services/controlPanel.service";

@Component({
    templateUrl: "users.component.html"
})

export class UsersComponent implements OnInit { 
    form!: FormGroup;
    openAccordion: Array<boolean> = [];
    isEditable: Array<boolean> = [];
    users?: Array<User>;
    loading = false;
    submitted = false;
    
    constructor(
        private controlPanelService: ControlPanelService,
        private formBuilder : FormBuilder,
    ) { 
        this.getTable();
    }
    
    ngOnInit() {
        this.form = this.formBuilder.group({
            username: [null, Validators.required],
            password: [null, [Validators.required, Validators.minLength(6)]],
            roleName: [null, Validators.required],
            email: [null, [Validators.requiredTrue, Validators.email]],
            socialSecurity: [null, Validators.required],
            phoneNumber: [null, Validators.required],
            address: [null, Validators.required],
            city: [null, Validators.required],
            country: [null, Validators.required],
            postalCode: [null, Validators.required],
        })
    }

    get values() {
        return this.form.controls;
    }

    getTable() {
        this.controlPanelService.getAllUsers()
            .subscribe({
                next: (data : User[]) => {
                    this.users = data;
                }
            })
    }

    delete(id: string) {
        this.controlPanelService.deleteUser(id)
            .subscribe({
                next: () => {
                    this.getTable();
                }
            });
    }

    update(id: string, i: number){
        this.submitted = true;
        this.loading = true;
        this.controlPanelService.updateUser(id, this.form.value)
            .subscribe({
                next: (data) => {
                    data = this.form.value;
                    this.isEditable[i] = !this.isEditable;
                    this.getTable();
                    this.loading = false;
                    this.submitted = false;    
                },
                error: error => {
                    console.log("error", error);
                    this.loading = false;  
                }
            });
    }

    getCurrentUser(i: number){
        this.form.controls['username'].setValue(this.users![i].username);
        this.form.controls['password'].setValue(null);
        this.form.controls['roleName'].setValue(this.users![i].role!.roleName);
        this.form.controls['email'].setValue(this.users![i].email);
        this.form.controls['socialSecurity'].setValue(this.users![i].socialSecurity);
        this.form.controls['phoneNumber'].setValue(this.users![i].phoneNumber);
        this.form.controls['address'].setValue(this.users![i].address);
        this.form.controls['city'].setValue(this.users![i].city);
        this.form.controls['country'].setValue(this.users![i].country);
        this.form.controls['postalCode'].setValue(this.users![i].postalCode);
    }
}