import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {BehaviorSubject, map, Observable} from "rxjs";
import axios from 'axios';

import { User } from '../_models/user.model';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  /**
   * return last value when subscribed
   * use next to gove it a value
   * used for services since services are rendered before the components use them
   * @private
   */
  private userSubject : BehaviorSubject<User | null>
  /**
   * this has no state and is run for each observer
   * observer cannot change value can only listen so using next is problematic
   */
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

  /**
   * returns the user object that is latest in the stream
   * so lets say an user logs out not gonna return the old one
   */
  public get userValue() {
    return this.userSubject.value;
  }

  /**
   * sends a post request to server
   * recieves the response and pipes it so we get user object
   * returns the logged in user object
   * @param username
   * @param password
   */
  loginHttp(username : string, password : string) {
    return this.http.post<User>(`${environment.apiUrl}/users/authenticate`,{username,password}).pipe(map(user => {
      //this is to store user in the local part as a session so user is not lost as page refreshes
      localStorage.setItem('user',JSON.stringify(user));
      //this is essentially setting the user observable to the new user not just local storage behaviordubject
      this.userSubject.next(user);
      return user;
    }))
  }

  /**
   * logs out user by removing user from local storage and setting the behavior subject to null
   */
  logoutHttp() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/account/login']);
  }

  /**
   * simple register method to basically send the user object from component to the server
   * @param user
   */
  registerHttp(user: User) {
    return this.http.post(`${environment.apiUrl}/users/register`,user);
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
      // data: {
      //   id: id
      // }
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
