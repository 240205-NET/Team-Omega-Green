import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from "rxjs";

import { User } from '../_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private userSubject : BehaviorSubject<User | null>
  private user : Observable<User | null>

  constructor(
    private router : Router
  ) {
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  login(username : string, password : string) {

  }

  logout() {

  }

  register(user : User) {

  }

  getCurrentUser() {
    return this.userSubject.value;
  }
  getAllUsers() {

  }

  getUserById(id : string) {

  }

  updateUserById(id : string, params : any) {

  }

  deleteUserById(id : string) {

  }
}
