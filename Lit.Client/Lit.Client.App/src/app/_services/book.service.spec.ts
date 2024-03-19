import { TestBed } from '@angular/core/testing';

import { BookService } from './book.service';
import { Book } from '../_models/book.model';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('BookService', () => {

  let service: BookService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
    service = TestBed.inject(BookService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  const randomIsbn = '1234';
  it('#getBookByIsbnAsync() should return value from promise', (done: DoneFn) => {
    service.getBookByIsbnAsync(randomIsbn).then((value) => {
      expect(value.title).toBe('BookTitle');
      expect(value.author).toBe('Ian Ortin');
      expect(value.isbn).toBe('1234');
      expect(value.categoryId).toBe(7);
    });
  });
  it('#getBookByIsbnAsync() should GET on /books/\$\{books\}', (done: DoneFn) => {
    service.getBookByIsbnAsync(randomIsbn).then(() => {
      const successRequest = httpMock.expectOne(service.bookUrl + `/books/${randomIsbn}`);
      expect(successRequest.request.method).toEqual('GET');
      successRequest.flush(null);
      httpMock.verify();
    });
  });

  const newBook : Book = {
    isbn: '8675309',
    title: 'A New Book',
    author: 'Jesses Girl',
    categoryId: 2,
    categoryName: 'some category'
  };
  it('#createBookAsync() should return a status code of 200', (done: DoneFn) => {
    service.createBookAsync(newBook).then((value) => {
      expect(value).toBe(200);
    });
  });
  it('#createBookAsync() should POST on /books', (done: DoneFn) => {
    service.createBookAsync(newBook).then(() => {
      const successRequest = httpMock.expectOne(service.bookUrl + '/books');
      expect(successRequest.request.method).toEqual('POST');
      successRequest.flush(null);
      httpMock.verify();
    });
  });

  const updateTargetId : string = '10';
  const params = {
    title: 'Tim is Cool',
    categoryId: 4
  }
  it('#updateBookByIdAsync() should return a status code of 200', (done: DoneFn) => {
    service.updateBookByIdAsync(updateTargetId, params).then((value) => {
      expect(value).toBe(200);
    });
  });
  it('#updateBookByIdAsync() should PUT on /books/\$\{id\}', (done: DoneFn) => {
    service.updateBookByIdAsync(updateTargetId, params).then(() => {
      const successRequest = httpMock.expectOne(service.bookUrl + `/books/${updateTargetId}`);
      expect(successRequest.request.method).toEqual('PUT');
      successRequest.flush(null);
      httpMock.verify();
    });
  });

  const deleteTargetId : string = '4';
  it('#deleteBookByIdAsync() should return a status code of 200', (done: DoneFn) => {
    service.deleteBookByIdAsync(deleteTargetId).then((value) => {
      expect(value).toBe(200);
    });
  });
  it('#deleteBookByIdAsync() should DELETE on /books/\$\{id\}', (done: DoneFn) => {
    service.deleteBookByIdAsync(deleteTargetId).then(() => {
      const successRequest = httpMock.expectOne(service.bookUrl + `/books/${deleteTargetId}`);
      expect(successRequest.request.method).toEqual('DELETE');
      successRequest.flush(null);
      httpMock.verify();
    });
  });

});
