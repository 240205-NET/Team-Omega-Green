import { Injectable } from '@angular/core';
import { Router } from '@angular/router';


import { Book } from '../_models/book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private router : Router) { 
    
  }
}

getAllBooks() {

}

getBookById(isbn : string) {

}

getReviewById(id : string) {

}

