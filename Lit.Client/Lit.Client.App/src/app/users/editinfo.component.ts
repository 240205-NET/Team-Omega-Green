import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-editinfo',
  templateUrl: './editinfo.component.html'
})
export class EditInfoComponent {
  form! : FormGroup;
  constructor(private router: Router, private route: ActivatedRoute, private accntSrvice: AccountService) {}

  ReturnToInfo() {
    this.router.navigate([':id']);

    
  }

  OnSubmit() {
    if(this.form.invalid) {
      return;
    }

  }
  
}
