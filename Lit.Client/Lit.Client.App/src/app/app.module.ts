import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BookDetailComponent } from './book-detail/book-detail.component'


@NgModule({
  declarations: [
    AppComponent,
    BookDetailComponent,
    
  ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

