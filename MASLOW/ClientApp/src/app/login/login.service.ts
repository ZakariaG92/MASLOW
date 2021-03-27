import { Inject, Injectable } from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {catchError, map} from "rxjs/operators";
import {Login, LoginResponse} from "./login.model";


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  token :string = null;
  userService: any;

  get isLogged(): boolean {
    return this.token != null;
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  logIn(email:String, password:String): Observable<Login> {
    const body = {
      email: email,
      password: password
    };

    return this.http.post<LoginResponse>(`${this.baseUrl}api/accounts/login`, JSON.stringify(body))
      .pipe(
        map(res => {
          if(res.token){
            //If auth is ok, save the token for api requests
            this.token = res.token;
          }
          else {
            this.token = null;
          }

          const result = new Login();
          result.auth = res.token != null;
          result.message = res.message;
          return result;
        }),
        catchError(this.handleError("logIn"))
      );
  }

  logOut(): Observable<Login>{
    //Remove token disable login
    this.token = null
    this.userService.user = undefined;

    const result = new Login();
    result.auth = false;
    result.message = "Déconnexion réussie";

    return of(result);
  }


  private handleError(operation: string, res?: HttpResponse<any>): (error: any) => Observable<Login> {
    return (error: any): Observable<Login> => {
      console.log(error);
      console.log(operation + ' issue : ' + error.message);

      const result = new Login();
      result.auth = false;
      result.message = error.error.message || error.error ;

      return of(result);
    };
  }
}
