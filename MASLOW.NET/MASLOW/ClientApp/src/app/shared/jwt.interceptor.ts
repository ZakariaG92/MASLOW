import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from '../login/login.service';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private loginService: LoginService ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(this.loginService.isLogged)
      //Si on est connecté, on ajoute notre token à nos requêtes
      return next.handle(
        request.clone({setHeaders: { Authorization: `Bearer ${this.loginService.token}`}})
      );

    //Sinon on renvoie la requête tel quel
    return next.handle(request);
  }
}
