import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule} from '@angular/router';

import { LoginComponent } from './login.component';
import { RegisterComponent} from './register.component';
import { LayoutComponent } from "./layout.component";

/**
 * create route to layoutcomponent and then display either login or register based on choice
 */
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


/**
 * account module imports the child routes and lets app routes use the built child routes
 */
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports : [ RouterModule ]
})
export class AccountRoutingModule { }
