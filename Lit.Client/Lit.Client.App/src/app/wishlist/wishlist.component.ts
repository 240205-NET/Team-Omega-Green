import { Component, OnInit } from '@angular/core';
import { BookService } from '../_services/book.service';
import { Book } from '../_models/book.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent {
  
  wishlist? : Book[];
  constructor (private router: Router, private bookservice: BookService) {  }

  ngOnInit() {
    // Grab wishlist contents and assign this.wishlist to it
    // this.wishlist = this.bookservice.wishlist.subscribe(next())
    this.wishlist = JSON.parse(localStorage.getItem('wishlist')!) as Book[];
    console.log(this.wishlist);
  }
  
}
