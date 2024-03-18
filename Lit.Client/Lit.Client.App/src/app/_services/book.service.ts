import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import axios from 'axios';


import { Book } from '../_models/book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private bookUrl : string;

  constructor (
    // private router : Router;
  )
  {
    this.bookUrl = 'https://omega-green.azurewebsites.net/api';
  }

  async getAllBooksAsync () {
    try {
      const response = await axios.get(this.bookUrl + '/books');
      console.log(response);
      return response.data as Book[];
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  async getBookByIdAsync (id : string) {
    try {
      const response = await axios.get(this.bookUrl + `/books/${id}`);
      console.log(response);
      return response.data as Book;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  async createBookAsync (book : Book) {
    try {
      const response = await axios.post(this.bookUrl + '/books', {
        title: book.title,
        author: book.author,
        isbn: book.isbn,
        categoryId: book.categoryId
      })
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
      const response = await axios.put(this.bookUrl + `/books/${id}`, {
        title: params.title,
        author: params.author,
        isbn: params.isbn,
        categoryId: params.categoryId
      })
      console.log(response);
      return response.status;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  async deleteBookByAsync (id : string) {
    try {
      const response = await axios.delete(this.bookUrl + `/books/${id}`, {
        data: {
          id: id
        }
      })
      console.log(response);
      return response.status;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

}

getAllBooks() {

}

getBookById(isbn : string) {

}

getReviewById(id : string) {

}

