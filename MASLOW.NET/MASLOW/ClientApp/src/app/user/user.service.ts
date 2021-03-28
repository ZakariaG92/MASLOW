import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
   }

   public getUser() : Observable <User> {
    return this.http.get<User>(this.baseUrl + 'weatherforecast');
   }
}

export interface User {
  id: string;
  username: string;
  email: string;
}
