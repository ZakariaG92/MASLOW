import { Component } from '@angular/core';
import { User, UserService } from './user.service';

@Component({
  selector: 'app-user-component',
  templateUrl: './user.component.html'
})
export class UserComponent {

  public users: User[];
  public currentCount = 0;

  constructor(private userService : UserService) {
    userService.getUser().subscribe(result => {
      this.users = result;
    }, error => console.error(error));

  }

  public incrementCounter() {
    this.currentCount++;
  }

  

}


