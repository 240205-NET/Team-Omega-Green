import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { ListUserComponent } from './listuser.component';
import { EditInfoComponent } from './editinfo.component'

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children : [
       {
        path: ':id', component: ListUserComponent
       },
       {
        path: 'edit/:id', component: EditInfoComponent
       }
    ]
  }
];
 

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
