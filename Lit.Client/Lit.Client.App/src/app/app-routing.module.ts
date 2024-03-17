import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LandingComponent } from './landing/landing.component';
import { HomeComponent } from './home/home.component';
import { BooklistComponent } from './booklist/booklist.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { LayoutComponent } from './account/layout.component';
import { AccountModule } from './account/account.module';
import { SearchResultsComponent } from './search-results/search-results.component';
import { UsersModule } from './users/users.module';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);

const userModule = () => import('./users/users.module').then(x => x.UsersModule);

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
  },
  { 
    path: 'search-results/:query',
    component: SearchResultsComponent },
  {
    path: 'account',
    loadChildren: accountModule
  },
  {
    path: 'users',
    loadChildren: userModule
  }
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }