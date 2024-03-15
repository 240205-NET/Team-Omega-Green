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

  navigateToAccount() {
    this.router.navigate(['account']);

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.router.navigate(['login'], { relativeTo: this.route });
    });
  }

  navigateToHome() {
    this.router.navigate(['home']);
  }

}
