import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import axios from 'axios';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { NavbarComponent } from 'src/app/navbar/navbar.component';
import { LandingComponent } from './landing/landing.component';
import { HomeComponent } from './home/home.component';
import { BooklistComponent } from './booklist/booklist.component';
import { RegisterComponent } from './account/register.component';
import { LayoutComponent } from './account/layout.component';
import { SearchboxComponent } from './searchbox/searchbox.component';
import { SearchResultsComponent } from './search-results/search-results.component';

import { ListUserComponent } from './users/listuser.component';
import { NavbarModule } from './navbar/navbar.module';





@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    HomeComponent,
    BooklistComponent,
    SearchboxComponent,
    SearchResultsComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    NavbarModule
  ],
  // exports:[NavbarModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
