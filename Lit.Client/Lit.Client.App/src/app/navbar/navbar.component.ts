import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(private router: Router, private route: ActivatedRoute, private accntSrvice: AccountService) {}

  hasUser() {
    if(this.accntSrvice.getCurrentUser()) {
      return true;
    }
    else {
      return false;
    }
  }

  navigateToLogin() {
    this.router.navigate(['account/login']);

    
  }

  navigateToRegister() {
    this.router.navigate(['account/register']);

    
  }

  navigateToHome() {
    this.router.navigate(['home']);
  }

  navigateToUserInfo() {
    this.router.navigate(['home']);
  }

}
