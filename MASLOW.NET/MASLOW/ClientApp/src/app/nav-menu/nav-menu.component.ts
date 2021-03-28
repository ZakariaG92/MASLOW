import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private loginService:LoginService,
    private router:Router) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(){
    this.loginService.logOut().subscribe(login =>{
      //On redirige vers la page de login
      this.router.navigate(['/login']);
    });
  }

  get isLogged(): boolean {
    return this.loginService.isLogged;
  }

}
