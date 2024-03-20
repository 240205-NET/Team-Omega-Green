import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {
  searchQuery: string = '';
  books: Book[] = [];
  filteredBooks: Book[] = []; // Array to hold filtered books!
  apiUrl: string = 'api/books';

  constructor(
    private route: ActivatedRoute,
    private httpClient: HttpClient
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.searchQuery = params['query'];
      this.fetchBooks(); // Call the method to fetch books!!
    });
  }

  fetchBooks(): void {
    this.httpClient.get<Book[]>(this.apiUrl).subscribe(
      (books: Book[]) => {
        this.books = books;
        this.filterBooks(); // Filter books after fetching!!!
        console.log(this.filteredBooks);
      },
      error => {
        console.error('Error fetching books:', error);
        this.searchQuery = `Error searching for: ${this.searchQuery}. Try again.`;
      }
    );
  }

  filterBooks(): void {
    if (this.searchQuery.trim() === '') {
      // If search query is empty, display all books.. actually this won't ever be called because I blocked empty search queries in the searchbox component but just in case...
      this.filteredBooks = this.books;
    } else {
      // Filter books based on search query
      this.filteredBooks = this.books.filter(book =>
        this.bookContainsQuery(book)
      );
    }
  }

  bookContainsQuery(book: Book): boolean {
    const query = this.searchQuery.toLowerCase();
    return (
      book.title.toLowerCase().includes(query) ||
      book.isbn.toLowerCase().includes(query) ||
      book.author.toLowerCase().includes(query)
    );
  }
  

}


interface Book {
  id: number;
  title: string;
  author: string;
  isbn: string;
  categoryId: number;
}
