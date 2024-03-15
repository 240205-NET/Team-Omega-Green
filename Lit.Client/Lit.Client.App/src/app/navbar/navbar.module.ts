import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar.component';
import { AccountService } from '../_services/account.service';



@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    AccountService
  ],
  exports: [
     NavbarComponent
  ]
})
export class NavbarModule { }
