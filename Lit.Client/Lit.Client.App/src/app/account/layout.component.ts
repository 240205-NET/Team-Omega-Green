import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../_services/account.service';

/**
 * took out the styles since using bootstrap
 * took out standalone since it has been declared in module
 * took out selector since page is rendered separately and not really used but can be implemented if needed
 */
@Component({
  templateUrl: './layout.component.html'
})
export class LayoutComponent {

  /**
   * initializes layout with router and the service methods to route and get data for all account components
   * @param router the router so layout can navigate to other pages
   * @param accountService service class to be passed in to use async methods and connect to server
   */
  constructor(
    private router : Router,
    private accountService : AccountService
  ) {
    /*
      if the user exists aka logged in the page should just redirect to home page
      shouldn't happen but just in case
     */
    if(this.accountService.getCurrentUser()) {
      this.router.navigate(['/']);
    }
  }
}
