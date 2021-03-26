import { Component } from '@angular/core';
import { User, UserService } from './user.service';

@Component({
  selector: 'app-user-component',
  templateUrl: './user.component.html'
})
export class UserComponent {

  public user: User;

  constructor(private userService : UserService) {
    userService.getUser().subscribe(result => {
      this.user = result;
    }, error => console.error(error));

  }

}


