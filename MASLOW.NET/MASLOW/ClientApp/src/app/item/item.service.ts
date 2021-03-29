import { HttpClient, HttpResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  public getItem() : Observable <Item[]> {
   return this.http.get<Item[]>(this.baseUrl + 'api/items');
  }

  public getSonsor(id: string, value: string) {
    return this.http.get(this.baseUrl + 'api/sersors/'+ id +'/'+ value, {responseType : 'text'});
  }

  public doAction(itemId: string, action: string) {
    const body = {
      itemId: itemId,
      action: action
    };
    return this.http.post(`${this.baseUrl}api/actions`, JSON.stringify(body));
  }
}

export interface Item {
  id: string;
  name: string;
  payload: object;
  actions: string[];
  values: string[];
}
