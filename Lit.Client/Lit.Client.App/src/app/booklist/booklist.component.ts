import { Component } from '@angular/core';
import axios from 'axios';

@Component({
  selector: 'app-booklist',
  // standalone: true,
  // imports: [],
  templateUrl: './booklist.component.html',
  styleUrls: ['./booklist.component.css']
})
export class BooklistComponent {
  
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

interface Book {
  id: number,
  title: string,
  author: string,
  isbn: string,
  categoryId: number
}

/*
function getAllBooks() {
  // change for database
  fetch("http://localhost:0000/api/Book")
  .then(

    //success
    (response) => response.json()
  )
  .then((json) => {
    //success
    AddtoTable(json)
  })
  .catch(
    //failure
    (e) => console.error(e)
  )
}*/

// async function getAllBooksAsyncAwait() {
//   //change for databse
//   const resStream = await fetch("http://localhost:0000/api/Book")
//   const resBody = await resStream.json()

//   AddtoTable(resBody)
// }

// function AddtoTable(dataArray) {
//   let table = document.getElementById('book-table')

//   for(row of dataArray) {
//     let newRow = table.insertRow(-1)

//     newRow.insertCell(0).innerText = row.BookName
//     newRow.insertCell(1).innerText = row.BookAuther
//     newRow.insertCell(2).innerText = row.PublishDate
//     newRow.insertCell(3).innerText = row.BookDetails
//   }
// }