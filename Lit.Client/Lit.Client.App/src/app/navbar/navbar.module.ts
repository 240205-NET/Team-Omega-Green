import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar.component';

import { AccountService } from '../_services/account.service';
import { SearchboxComponent } from '../searchbox/searchbox.component';
import { WishlistComponent } from '../wishlist/wishlist.component';

@NgModule({
  declarations: [NavbarComponent,SearchboxComponent],
  imports: [
    CommonModule,
  ],
  exports: [
     NavbarComponent
  ]
})
export class NavbarModule { }
