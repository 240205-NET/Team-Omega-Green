import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';
import { BooklistComponent } from './booklist.component';
import { NavbarModule } from '../navbar/navbar.module';




@NgModule({
  declarations: [BooklistComponent],
  imports: [
    CommonModule,
    NavbarComponent
  ]
})
export class BooklistModule { }
