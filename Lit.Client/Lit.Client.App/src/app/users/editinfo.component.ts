import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../_models/user.model';


@Component({
  selector: 'app-editinfo',
  templateUrl: './editinfo.component.html'
})
export class EditInfoComponent implements OnInit{
  form! : FormGroup;
  id? : string | any;
  user? : User | null;
  constructor(private router: Router, private route: ActivatedRoute, private accntSrvice: AccountService, private frmBuild: FormBuilder) {
    this.user = this.accntSrvice.userValue;
  }

  ngOnInit(): void {
      this.id = this.accntSrvice.userValue?.userId;
      this.form = this.frmBuild.group({
        username: [this.user?.username, Validators.required],
        firstName: [this.user?.firstName, Validators.required],
        lastName: [this.user?.lastName, Validators.required],
        email: [this.user?.email, Validators.required]
      
      })

      console.log(this.id);
  }                                                                                                                                                                                                                                                                                                                                                              

  ReturnToInfo() {
    this.router.navigate(['users/:id']);
  }

  OnSubmit() {
    if(this.form.invalid) {
      return;
    }
    console.log(this.form.value);
    console.log(this.id);
    this.accntSrvice.updateUser(this.id!, this.form.value).pipe().subscribe({
      next:() => {
        this.router.navigateByUrl('users/:id')
      }
    })

  }
  
}
