import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../_services/book.service';
import { Book } from '../_models/book.model';


@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls:['./book-detail.component.css']
}) 
export class BookDetailComponent implements OnInit {

  book!: Book;
  
  constructor(private route: ActivatedRoute, private bookService: BookService) { }


  ngOnInit(): void {
    this.route.params.subscribe(async params => {
      const bookId = params['id']; // Get book ID from route parameters
      this.book = await this.bookService.getBookByIdAsync(bookId); // Fetch book details
    });
    
  }

  addToWishlist(): void {
    // Create a new book object for wishlist
    const wishlistBook: Book = {
      title: this.book.title,
      author: this.book.author,
      isbn: this.book.isbn,
      categoryName: this.book.categoryName,
      // Add other properties as needed
    };

    // Do something with the wishlistBook object (e.g., save it to a wishlist array)
    console.log('Added to wishlist:', wishlistBook);
  }

}
