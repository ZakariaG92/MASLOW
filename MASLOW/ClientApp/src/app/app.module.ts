import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserComponent } from './user/user.component';
import { ItemComponent } from './item/item.component';
import { LoginComponent } from './login/login.component';


import { CommonModule } from '@angular/common';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {ReactiveFormsModule} from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginGuard } from './shared/login.guard';
import { LoginService } from './login/login.service';
import { HeaderInterceptor } from './shared/header.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    UserComponent,
    ItemComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    MatIconModule,
    MatCardModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [LoginGuard] },
      { path: 'user', component: UserComponent, canActivate: [LoginGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ],
    { relativeLinkResolution: 'legacy' }),
    BrowserAnimationsModule
  ],
  providers: [
    LoginService,
    {provide: HTTP_INTERCEPTORS, useClass: HeaderInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
