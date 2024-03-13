import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavbarComponent } from 'src/app/navbar/navbar.component';
import { BookDetailComponent } from './book-detail/book-detail.component'


const appRoutes: Routes = [
  {path: 'book-detail/:id', component: BookDetailComponent}
]
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent
    BookDetailComponent,
    
  ],
  imports: [
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
