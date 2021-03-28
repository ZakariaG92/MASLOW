import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  public getItem() : Observable <Item[]> {
   return this.http.get<Item[]>(this.baseUrl + 'weatherforecast');
  }
}

export interface Item {
  id: string;
  name: string;
  email: string;
}
