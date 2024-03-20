import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import axios from 'axios';


import { Book } from '../_models/book.model';
import { BehaviorSubject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  public bookUrl : string;
  public wishlist : Observable<Book[]>;
  private wishlistSubject: BehaviorSubject<Book[]>;

  constructor (
    // private router : Router;

  )
  {
    this.bookUrl = 'https://omega-green.azurewebsites.net/api';
    //this.bookUrl = 'https://6cbff109-9d1e-4268-83f0-e763975c271d.mock.pstmn.io'
    let tempBookArray : Book[] = [] 
    localStorage.setItem('wishlist', JSON.stringify(tempBookArray));
    this.wishlistSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('wishlist')!));
    this.wishlist = this.wishlistSubject.asObservable();
  }

  async getAllBooksAsync() : Promise<Book[]> {
    try {
      let options = {
        method: 'GET',
        url: this.bookUrl + '/books',
        mode: 'no-cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=UTF-8'
        }        
      };
      const response = await axios(options);
      console.log(response);
      return await response.data as Book[];
    }
    catch (error) {
      console.log(error);
      return [] as Book[];
    }
  }

  async getBookByIdAsync (id : string) : Promise<Book> {
    try {
      let options = {
        method: 'GET',
        url: this.bookUrl + `/books/isbn/${id}`,
        mode: 'no-cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=UTF-8'
        }
      }
      const response = await axios(options);
      console.log(response);
      return await response.data as Book;
    }
    catch (error) {
      console.log(error);
      return new Book;
    }
  }

  async getBookByIsbnAsync (isbn : string) : Promise<Book> {
    try {
      let options = {
        method: 'GET',
        url: this.bookUrl + `/books/${isbn}`,
        mode: 'no-cors',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json;charset=UTF-8'
        }
      }
      const response = await axios(options);
      console.log(response);
      return await response.data as Book;
    }
    catch (error) {
      console.log(error);
      return new Book;
    }
  }

  async createBookAsync (book : Book) {
    try {
      let options = {
        method: 'POST',
        url: this.bookUrl + '/books',
        mode: 'no-cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: {
          title: book.title,
          author: book.author,
          isbn: book.isbn,
          categoryId: book.categoryId
        }
      }
      const response = await axios(options);
      console.log(response);
      return response.status;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  async updateBookByIdAsync (id : string, params : any) {
    try {
      let options = {
        method: 'PUT',
        url: this.bookUrl + `/books/${id}`,
        mode: 'no-cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: {
          title: params.title,
          author: params.author,
          isbn: params.isbn,
          categoryId: params.categoryId
        }
      }
      const response = await axios(options);
      console.log(response);
      return response.status;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  async deleteBookByIdAsync (id : string) {
    try {
      let options = {
        method: 'DELETE',
        url: this.bookUrl + `/books/${id}`,
        mode: 'no-cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: {
          id: id
        }
      }
      const response = await axios(options);
      console.log(response);
      return response.status;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  addBookToWishlist(book: Book) {
    let bookArray  = JSON.parse(localStorage.getItem('wishlist')!);
    let book2 : Book = {
      title: "randomnames",
      author: "randomguy",
      isbn: "112432423",
      categoryName: "surprise",
    }
    console.log(bookArray)
    bookArray.push(book);
    console.log(JSON.stringify(bookArray))
    localStorage.setItem('wishlist', JSON.stringify(bookArray));
    this.wishlistSubject.next(bookArray as Book[])
    
    // bookArray.
    // localStorage.setItem('wishlist', JSON.)
  }

}

// getAllBooks() {

// }

// getBookById(isbn : string) {

// }

// getReviewById(id : string) {

// }

