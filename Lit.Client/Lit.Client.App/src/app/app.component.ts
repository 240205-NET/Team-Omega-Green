import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from './_services/account.service';
import { User } from './_models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css', './boostrap.css']
})
export class AppComponent {
  title = 'Lit.Client.App';
  user? : (User | null);

  constructor(private accntService: AccountService) {
    this.accntService.user.subscribe(user => this.user = user)
  }

  logout() {
    this.accntService.logoutHttp();
  }


}
