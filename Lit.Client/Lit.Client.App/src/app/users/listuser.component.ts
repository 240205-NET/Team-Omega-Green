import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user.model';


@Component({
  selector: 'app-listuser',
  templateUrl: './listuser.component.html'
})
export class ListUserComponent implements OnInit{
  user? : User | null;

  
  
  constructor(private router: Router, private accntSrvice: AccountService) {
     this.user = this.accntSrvice.userValue;
  }

  //
  ngOnInit(): void {
      // this.accntSrvice.getCurrentUser().subscribe(user => this.user = user);
      
  }
 

  //if doesn't work use routerLink!
  EditUserInfo() {
    this.router.navigate(['users/edit/:id']);
  }

}
