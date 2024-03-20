import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from './_services/account.service';
import { User } from './_models/user.model';
import { Book } from './_models/book.model';
import { BookService } from './_services/book.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css', './boostrap.css']
})
export class AppComponent {
  title = 'Lit.Client.App';
  user? : (User | null);
  wishlist? : Book[];

  constructor(private accntService: AccountService, private bookSrvice: BookService) {
    this.accntService.user.subscribe(user => this.user = user)
    this.bookSrvice.wishlist.subscribe(wishlist => this.wishlist)
  }

  logout() {
    this.accntService.logoutHttp();
  }


}
