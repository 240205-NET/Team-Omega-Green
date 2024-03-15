import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar.component';
import { AccountService } from '../_services/account.service';
import { SearchboxComponent } from '../searchbox/searchbox.component';



@NgModule({
  declarations: [NavbarComponent,SearchboxComponent]
  imports: [
    CommonModule,
    AccountService
  ],
  exports: [
     NavbarComponent
  ]
})
export class NavbarModule { }
