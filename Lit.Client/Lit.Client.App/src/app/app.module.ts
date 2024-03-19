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

import { SearchResultsComponent } from './search-results/search-results.component';

import { ListUserComponent } from './users/listuser.component';
import { NavbarModule } from './navbar/navbar.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import {fakeBackendProvider} from "./_interceptors/fake-backend";
import {JwtInterceptor} from "./_interceptors/jwt.interceptor";


@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    HomeComponent,
    BooklistComponent,
    SearchResultsComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    NavbarModule,
    HttpClientModule
  ],
  // exports:[NavbarModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },

    // provider used to create fake backend
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
