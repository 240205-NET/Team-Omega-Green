import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { first } from "rxjs/operators";

import { AccountService } from "../_services/account.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  form! : FormGroup;
  loading = false;
  submitted = false;

  constructor (
    private formBuilder : FormBuilder,
    private route : ActivatedRoute,
    private router : Router,
    private accountService : AccountService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      firstName : ['', Validators.required],
      lastName : ['', Validators.required],
      username : ['', Validators.required],
      password : ['', Validators.required],
      email : ['', Validators.required]
    })
  }

  onSubmit() {
    this.submitted = true;

    if(this.form.invalid) {
      return;
    }

    this.loading = true;

    
console.log(this.accountService.register(this.form.value).pipe(first())  + "hey");
    // this.accountService.register(this.form.value).pipe(first()).subscribe({
    //   next : () => {
    //     this.router.navigate(['../login'], {relativeTo: this.route});
    //   },
    //   error : error => {
    //     this.loading = false;
    //   }
    // })
  }
}
