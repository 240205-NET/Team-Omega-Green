import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user.model';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-listuser',
  templateUrl: './listuser.component.html',
  styleUrls: ['./listuser.component.css']
})
export class ListUserComponent implements OnInit{
  user? : User;
  
  constructor(private router: Router, private accntSrvice: AccountService) {}

  //
  ngOnInit(): void {
      // this.accntSrvice.getCurrentUser().subscribe(user => this.user = user);
  }
 

  //if doesn't work use routerLink!
  EditUserInfo() {
    this.router.navigate(['edit/:id']);
  }

}
