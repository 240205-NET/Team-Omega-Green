import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from "rxjs";
import axios from 'axios';

import { User } from '../_models/user.model';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private userSubject : BehaviorSubject<User | null>
  public user : Observable<User | null>
  private loginUrl : string

  constructor(
    private router : Router,
    private http : HttpClient
  ) {
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
    this.loginUrl = 'https://omega-green.azurewebsites.net/api/auth';
  }

  loginAxios(userName : string, passWord : string) {
    axios.post(this.loginUrl + '/login', {
      username: userName,
      password: passWord
    })
    .then ((response) => {
      // Not sure if this works until I can get to testing

      // this.user = response.data.map(item  => {
      //   id: item.Id,
      //   username: item.username,
      //   password: item.password,
      //   firstName: item.FirstName,
      //   lastName: item.LastName,
      //   token: item.Token
      // });

      this.user = response.data;
      localStorage.setItem('user', JSON.stringify(this.user));
      this.userSubject.next(this.user as User);
      return this.user;
      // Response handling may change after testing with mock data and/or database
      // console.log(response);
      // return response.data as User;
    }, (error) => {
      console.log(error);
    });
  }

  logoutAxios() {
    // Call the logout endpoint on the server if you have one (optional)
    fetch('/api/auth/logout', { method: 'POST' })
        .then(() => {
            // Clear the token from local storage, cookies, or wherever it's stored
            localStorage.removeItem('token');
            // Redirect the user to the login page or home page as necessary
            window.location.href = '/login';
        });
}

  registerAxios(user : User) {
    axios.post(this.loginUrl + '/user', {
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

  getAllUsersAxios() {
    axios.get(this.loginUrl + '/user')
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.data as User[];
    }, (error) => {
      console.log(error);
    });
  }

  getUserByIdAxios(id : string) {
    axios.get(this.loginUrl + `/user/${id}`)
    .then((response) => {
      // Response handling may change after testing with mock data and/or database
      console.log(response);
      return response.data as User;
    }, (error) => {
      console.log(error);
    });
  }

  updateUserByIdAxios(id : string, params : any) {
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

  deleteUserByIdAxios(id : string) {
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
