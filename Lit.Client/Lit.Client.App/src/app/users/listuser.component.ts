import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user.model';


@Component({
  selector: 'app-listuser',
  templateUrl: './listuser.component.html'
})
export class ListUserComponent implements OnInit{
  user? : User;

  
  
  constructor(private router: Router, private accntSrvice: AccountService) {
    this.user = {
      id: "1",
      firstName: "Stephen",
      lastName: "Sams",
      username : "ssams01",
      password : "str@ngpa55word",
      email : "madeup@email.com",
      token : ""
    };
  }

  //
  ngOnInit(): void {
      // this.accntSrvice.getCurrentUser().subscribe(user => this.user = user);
      
  }
 

  //if doesn't work use routerLink!
  EditUserInfo() {
    this.router.navigate(['edit/:id']);
  }

}
