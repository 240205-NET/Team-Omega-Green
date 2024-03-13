import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LandingComponent } from './landing/landing.component';
import { HomeComponent } from './home/home.component';
import { BooklistComponent } from './booklist/booklist.component';
import { BookDetailComponent } from './book-detail/book-detail.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'landing',
    component: LandingComponent
  },
  {
    path: 'booklist',
    component: BooklistComponent
  },
  {
    path: 'book-detail/:id',
    component: BookDetailComponent
  }
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }