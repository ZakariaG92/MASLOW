import { Component } from '@angular/core';
import { Item, ItemService } from './item.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent {

  public items: Item[];

  constructor(private userService : ItemService) {
    userService.getItem().subscribe(result => {
      this.items = result;
    }, error => console.error(error));

  }


}
