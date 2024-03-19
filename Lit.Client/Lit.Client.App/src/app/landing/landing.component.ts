import { Component } from '@angular/core';
import {User} from "../_models/user.model";
import {AccountService} from "../_services/account.service";

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  user : User | null;

  constructor(private accountService:AccountService) {
    this.user = this.accountService.userValue;
  }
}
