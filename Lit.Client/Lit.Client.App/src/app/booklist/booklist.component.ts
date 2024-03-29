import { Component, OnInit } from '@angular/core';
import axios from 'axios';
import { BookService } from '../_services/book.service';
import { Book } from '../_models/book.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-booklist',
  // standalone: true,
  // imports: [],
  templateUrl: './booklist.component.html',
  styleUrls: ['./booklist.component.css']
})
export class BooklistComponent implements OnInit
{
  books? : Book[];
  constructor (private router: Router, private bookservice: BookService)  {  }

  async ngOnInit(): Promise<void> {
    this.books = await this.bookservice.getAllBooksAsync()
  }

  navigateToDetails() {
    this.router.navigate(['book-detail/:id'])
  }

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