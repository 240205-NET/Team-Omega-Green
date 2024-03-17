import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from "rxjs";
import axios from 'axios';

import { User } from '../_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {


  public userSubject : BehaviorSubject<User | null>
  public user : Observable<User | null>
  public loginUrl : string


  constructor(
    private router : Router
  ) {
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
    this.loginUrl = 'https://omega-green.azurewebsites.net';
  }

  login(userName : string, passWord : string) {
    axios.post(this.loginUrl + '/login', {
      username: userName,
      password: passWord
    })
    .then ((response) => {
      // Not sure if this works until I can get to testing
      /*
      this.user = response.data.map(item => {
        id: item.Id,
        username: item.username,
        password: item.password,
        firstName: item.FirstName,
        lastName: item.LastName,
        token: item.Token
      });
      */
      this.user = response.data;
      // Response handling may change after testing with mock data and/or database
      console.log(response);      
      return response.data as User;
    }, (error) => {
      console.log(error);
    });
  }

  logout() {
    // Not sure exactly how we want to handle this
  }

  register(user : User) {
    axios.post(this.loginUrl + '/user', {
      id: user.id,
      username: user.username,
      password: user.password,
      firstName: user.firstName,
      lastName: user.lastName,
      token: user.token
    })
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.status;
    }, (error) => {
      console.log(error);
    });
  }

  getCurrentUser() {
    return this.userSubject.value;
  }

  getAllUsers() {
    axios.get(this.loginUrl + '/user')
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.data as User[];
    }, (error) => {
      console.log(error);
    });
  }

  getUserById(id : string) {
    axios.get(this.loginUrl + `/user/${id}`)
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.data as User;
    }, (error) => {
      console.log(error);
    });
  }

  updateUserById(id : string, params : any) {
    axios.put(this.loginUrl + `/user/${id}`, {
      username: params.username,
      password: params.password,
      firstName: params.firstName,
      lastName: params.lastName,
      token: params.token
    })
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.status;
    }, (error) => {
      console.log(error);
    });
  }

  deleteUserById(id : string) {
    axios.delete(this.loginUrl + `/user/${id}`, {
      data: {
        id: id
      }
    })
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.status;
    }, (error) => {
      console.log(error);
    })
  }
}
