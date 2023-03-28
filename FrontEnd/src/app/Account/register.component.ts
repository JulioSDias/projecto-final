import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { FormBuilder, FormGroup, Validators} from "@angular/forms";
import { ToastrService } from "ngx-toastr";

import { AccountService } from "../Shared/Services/account.service";

@Component({
    templateUrl: 'register.component.html'
})

export class RegisterComponent implements OnInit{ 
    form!: FormGroup;
    submitted = false;
    loading = false;

    constructor(
        private router : Router,
        private route : ActivatedRoute,
        private formBuilder : FormBuilder,
        private accountService : AccountService,
        private toastr : ToastrService
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(6)]],
            firstname: ['', Validators.required],
            lastname: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            socialSecurity: ['', Validators.required],
            phoneNumber: ['', Validators.required],
            address: ['', Validators.required],
            city: ['', Validators.required],
            country: ['', Validators.required],
            postalCode: ['', Validators.required],
        })
    }
    
    get values() {
        return this.form.controls;
    }

    OnSubmit() {
        this.submitted = true;
        
        if(this.form.invalid){
            return;
        }

        this.loading = true;
        this.accountService.register(this.form.value)
            .subscribe({
                next: () => {
                        console.log("success");
                        this.toastr.success("Account created!", "SUCCESS", {timeOut:3000});
                        this.router.navigate(['../login'], {relativeTo: this.route});
                },
                error: error => {
                        console.log("error", error);
                        this.loading = false;  
                }

            });
    }
}