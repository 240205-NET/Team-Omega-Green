import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { first } from 'rxjs/operators';

import {AccountService} from "../_services/account.service";

/**
 * removed styles and standalone cause of module existing
 * selector can be implemented but pages dont need to be reproduced so should be good
 */
@Component({
  templateUrl: './login.component.html',
})

export class LoginComponent {

  /**
   * initializes form
   * loading - boolean to check if website is trying to get back something
   *  (want to use async pipe instead but still learning how to use that)
   * submitted - this boolean is used to check if the inputs have actually been submitted
   *  so we can use it to check and display validator errors
   */
  form! : FormGroup;
  loading = false;
  submitted = false;

  /**
   * initializes form active route router and the service
   * @param formBuilder the form to login
   * @param route activated route from authguard so we can restrict non logged in user from getting access to other users pages
   * @param router the router to navigate routes
   * @param accountService the service with server connection methods
   */
  constructor(
    private formBuilder : FormBuilder,
    private route : ActivatedRoute,
    private router : Router,
    private accountService : AccountService
  ) { }

  /**
   * if component renders it starts a form with username and password with validators to check
   */
  ngOnInit() {
    this.form = this.formBuilder.group({
      username : ['', Validators.required],
      password : ['', Validators.required]
    });
  }

  /**
   * if component submits i.e. form exits with inputs sets submitted to true
   * and tries loggin in with service method
   */
  onSubmit() {
    this.submitted = true;

    /**
     * if validators return errors just stop the login process otherwise set loading to true
     * so user can press login button and call service login method
     */
    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    /**
     * calls service login method with user inputted username and password
     * uses pipe to take the first object from the object which is id and
     * uses subscribe to send this id to the activated route to either go to userhomepage or go back home
     *
     */
    this.accountService.login(this.form.controls['username'].value, this.form.controls['password'].value);
    // this.accountService.login(this.form.controls['username'].value, this.form.controls['password'].value).pipe(first()).subscribe({
    //   next : () => {
    //     this.router.navigateByUrl(this.route.snapshot.queryParams['returnUrl'] || '/');
    //   },
    //   error : error => {
    //     /*
    //     for some reason if data was not available set loading to false
    //     have to print error message
    //      */
    //     this.loading = false;
    //   }
    // })
  }
}
