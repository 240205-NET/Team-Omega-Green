import { Component } from '@angular/core';

@Component({
  selector: 'app-booklist',
  standalone: true,
  imports: [],
  templateUrl: './booklist.component.html',
  styleUrl: './booklist.component.css'
})
export class BooklistComponent {

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

async function getAllBooksAsyncAwait() {
  //change for databse
  const resStream = await fetch("http://localhost:0000/api/Book")
  const resBody = await resStream.json()

  AddtoTable(resBody)
}

function AddtoTable(dataArray) {
  let table = document.getElementById('book-table')

  for(row of dataArray) {
    let newRow = table.insertRow(-1)

    newRow.insertCell(0).innerText = row.BookName
    newRow.insertCell(1).innerText = row.BookAuther
    newRow.insertCell(2).innerText = row.PublishDate
    newRow.insertCell(3).innerText = row.BookDetails
  }
}
