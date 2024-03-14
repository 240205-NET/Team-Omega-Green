import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { first } from 'rxjs/operators';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent {
  form! : FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder : FormBuilder,
    private route : ActivatedRoute,
    private router : Router
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      username : ['', Validators.required],
      password : ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.accountService.login(this.form.controls.username.value, this.form.controls.password.value).pipe(first()).subscribe({
      next : () => {
        this.router.navigateByUrl(this.route.snapshot.queryParams['returnUrl'] || '/');
      }
    })
  }
}
