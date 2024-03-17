import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListUserComponent } from './listuser.component';
import { EditInfoComponent } from './editinfo.component';
import { LayoutComponent } from './layout.component';
import { UsersRoutingModule } from './users-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

import { NavbarModule } from '../navbar/navbar.module';



@NgModule({
  declarations: [
    ListUserComponent,
    EditInfoComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    ReactiveFormsModule,
    NavbarModule
  ]
})
export class UsersModule { }
