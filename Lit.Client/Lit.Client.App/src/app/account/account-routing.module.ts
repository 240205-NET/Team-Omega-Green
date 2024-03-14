import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule} from '@angular/router';

import { LoginComponent } from './login.component';
import { RegisterComponent} from './register.component';

const routes : Routes = [
  {
    path : '',
    component : LayoutComponent,
    children : [
      {path : 'login', component : LoginComponent},
      {path : 'register', component : RegisterComponent}
    ]
  }
];



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports : [ RouterModule ]
})
export class AccountRoutingModule { }
