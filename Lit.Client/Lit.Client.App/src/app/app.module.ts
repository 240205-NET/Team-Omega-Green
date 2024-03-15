import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavbarComponent } from 'src/app/navbar/navbar.component';
import { LandingComponent } from './landing/landing.component';
import { HomeComponent } from './home/home.component';
import { BooklistComponent } from './booklist/booklist.component';
import { RegisterComponent } from './account/register.component';
import { LayoutComponent } from './account/layout.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LandingComponent,
    HomeComponent,
    BooklistComponent,
    RegisterComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule
  ],
  exports:[NavbarComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
