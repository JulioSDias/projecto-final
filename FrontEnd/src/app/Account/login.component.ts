import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";


import { AccountService } from "../Shared/Services/account.service";

@Component({
    templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
    form!: FormGroup;
    submitted = false;
    loading = false;
    
    constructor(
        private accountService: AccountService,
        private formBuilder: FormBuilder,
        private router : Router,
        private route : ActivatedRoute,
    ) { }
        
    
    ngOnInit(){
        this.form = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
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
        this.accountService.login(this.values['username'].value, this.values['password'].value)
            .subscribe({
                next: () => {
                    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
                    this.router.navigateByUrl(returnUrl);
                },
                error: error => {
                    console.log(error);
                    this.loading = false;
                }
            });
    }
 }